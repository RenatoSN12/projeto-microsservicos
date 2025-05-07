using Emitix.StockService.Common;
using Emitix.StockService.Data.UnitOfWork;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;
using Emitix.StockService.Exceptions;
using Emitix.StockService.Mappers;
using Emitix.StockService.Models;
using Emitix.StockService.Repositories;
using FluentValidation;

namespace Emitix.StockService.Services;

public class StockService(
    IProductStockRepository repository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateProductStockDto> validator) : IStockService
{
    public async Task<Response<ProductStockDto>> CreateProductStock(CreateProductStockDto request)
    {
        try
        {
            var productEntity = request.ToEntity();
            await repository.AddAsync(productEntity);
            await unitOfWork.CommitAsync();
            return Response<ProductStockDto>.Success(productEntity.ToDto());
        }
        catch (Exception e)
        {
            return Response<ProductStockDto>.Error(null, e.Message, 500);
        }
    }
    
    private async Task<List<string>> ValidateStockUpdate(List<UpdateProductStockDto> request)
    {
        var errors = new List<string>();
        foreach (var item in request)
        {
            var result = await validator.ValidateAsync(item);
            if (!result.IsValid)
                errors.AddRange(result.Errors.Select(x => x.ErrorMessage));
        }
        return errors;
    }
    
    public async Task<Response<List<ProductStockDto>>> UpdateProductStock(List<UpdateProductStockDto> request)
    {
        try
        {
            var validationError = await ValidateStockUpdate(request);
            if (validationError.Count > 0)
                return Response<List<ProductStockDto>>.Error(
                    null,
                    string.Join("\n\n", validationError),
                    400
                );

            var codes = request.Select(x => x.ProductCode).ToArray();
            var stocks = await repository.GetByListProductCodesAsync(codes);

            var unfoundStocks = CheckUnfoundedStocks(codes, stocks);
            if (unfoundStocks.Length != 0)
                return Response<List<ProductStockDto>>.Error(null,
                    $"Estoque não encontrado para os seguintes produtos: {string.Join(", ", unfoundStocks)}", 400);

            var result = UpdateProductStockModels(request, stocks);
            if (result.Length > 0)
            {
                var message = "Falha ao atualizar o estoque:\n\n" + string.Join("\n\n", result);
                return Response<List<ProductStockDto>>.Error(null, message, 400);
            }
            await unitOfWork.CommitAsync();
            return Response<List<ProductStockDto>>.Success(stocks.Select(x=>x.ToDto()).ToList());
        }
        catch (Exception e)
        {
            return Response<List<ProductStockDto>>.Error(null, e.Message, 500);
        }
    }

    public async Task<Response<ProductStockDto>> GetStockByProductCode(string productCode)
    {
        try
        {
            var productStock = await repository.GetByProductCodeAsync(productCode);
            return productStock == null 
                ? UnfoundedStockResponse(productCode) 
                : Response<ProductStockDto>.Success(productStock.ToDto());
        }
        catch (Exception e)
        {
            return Response<ProductStockDto>.Error(null, e.Message, 500);
        }
    }

    private string[] UpdateProductStockModels(List<UpdateProductStockDto> request, List<ProductStock> stocks)
    {
        var errors = new List<string>();
        foreach (var item in request)
        {
            try
            {
                var stock = stocks.First(s => s.ProductCode == item.ProductCode);
                stock.Update(item.Quantity, item.MovementType);
                repository.Update(stock);
            }
            catch (DomainException ex)
            {
                errors.Add(ex.Message);
            }
        }
        return errors.ToArray();
    }
    
    private string[] CheckUnfoundedStocks(string[] requestedCodes, List<ProductStock> foundStocks)
        => requestedCodes.Except(foundStocks.Select(s => s.ProductCode)).ToArray();
    
    private Response<ProductStockDto> UnfoundedStockResponse(string productCode)
        => Response<ProductStockDto>.Error(null,
            $"Nenhum estoque ativo foi encontrado para o produto com o código {productCode}",
            404
        );
    
}