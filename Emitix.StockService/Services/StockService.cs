using Emitix.StockService.Common;
using Emitix.StockService.Common.Extensions;
using Emitix.StockService.Data.UnitOfWork;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;
using Emitix.StockService.Exceptions;
using Emitix.StockService.Mappers;
using Emitix.StockService.Repositories;
using FluentValidation;

namespace Emitix.StockService.Services;

public class StockService(
    IProductStockRepository repository,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider) : IStockService
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

    public async Task<Response<List<ProductStockDto>>> UpdateProductStock(List<UpdateProductStockDto> request)
    {
        try
        {
            var validator = serviceProvider.GetRequiredService<IValidator<UpdateProductStockDto>>();
            foreach (var item in request)
            {
                var result = await validator.ValidateAsync(item);
                if (!result.IsValid)
                    return Response<List<ProductStockDto>>.Error(null, result.Errors.ToMessageString(), 400);
            }

            var codes = request.Select(x => x.ProductCode).ToArray();
            var stocks = await repository.GetByListProductCodesAsync(codes);

            if (stocks.Count != request.Count)
            {
                var unfoundStocks = codes.Except(stocks.Select(s => s.ProductCode));
                return Response<List<ProductStockDto>>.Error(null,
                    $"Estoque não encontrado para os seguintes produtos: {string.Join(", ", unfoundStocks)}", 400);
            }

            var domainErrors = new List<string>();
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
                    domainErrors.Add(ex.Message);
                }
            }
            
            if (domainErrors.Count > 0)
            {
                var message = "Falha ao atualizar o estoque:\n\n" + string.Join("\n\n", domainErrors);
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
                ? NotFoundStockResponse(productCode) 
                : Response<ProductStockDto>.Success(productStock.ToDto());
        }
        catch (Exception e)
        {
            return Response<ProductStockDto>.Error(null, e.Message, 500);
        }
    }

    public async Task<Response<bool>> VerifyStockAvailability(List<ProductStockRequestDto> request)
    {
        try
        {
            var productCodes = request.Select(r => r.ProductCode).ToArray();
            var stocks = await repository.GetByListProductCodesAsync(productCodes);
            if (stocks.Count != productCodes.Length)
            {
                var unfoundedStocks = productCodes.Except(stocks.Select(c => c.ProductCode));
                return Response<bool>.Error(false,
                    $"Não foram encontrados estoques cadastrados para os seguintes estoques informados: {string.Join(",", unfoundedStocks)}",
                    400);
            }
            
            var insufficientStocks = (from item in request
                                                let stock = stocks.FirstOrDefault(x => x.ProductCode == item.ProductCode)
                                                where stock.Quantity < item.Quantity 
                                                select $"{item.ProductCode} (necessário: {item.Quantity}, disponível: {stock.Quantity})").ToList();

            if (insufficientStocks.Count > 0)
            {
                return Response<bool>.Error(false,
                    $"Estoque insuficiente para os produtos: {string.Join(", ", insufficientStocks)}",
                    400);
            }
            
            return Response<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Response<bool>.Error(false, e.Message, 500);
        }
    }

    private Response<ProductStockDto> NotFoundStockResponse(string productCode)
        => Response<ProductStockDto>.Error(null,
            $"Nenhum estoque ativo foi encontrado para o produto com o código {productCode}",
            404
        );
}