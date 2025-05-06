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

    public async Task<Response<ProductStockDto>> UpdateProductStock(UpdateProductStockDto request)
    {
        try
        {
            var validator = serviceProvider.GetRequiredService<IValidator<UpdateProductStockDto>>();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return Response<ProductStockDto>.Error(null, result.Errors.ToMessageString(), 400);


            var productStock = await repository.GetByProductCodeAsync(request.ProductCode);
            if (productStock == null)
                return NotFoundStockResponse(request.ProductCode);

            productStock.Update(request.Quantity, request.MovementType);
            repository.Update(productStock);
            await unitOfWork.CommitAsync();
            return Response<ProductStockDto>.Success(productStock.ToDto());
        }
        catch (DomainException ex)
        {
            return Response<ProductStockDto>.Error(null, ex.Message, 400);
        }
        catch (Exception e)
        {
            return Response<ProductStockDto>.Error(null, e.Message, 500);
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

    public async Task<Response<bool>> VerifyStockAvailability(List<ProductStockRequest> request)
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