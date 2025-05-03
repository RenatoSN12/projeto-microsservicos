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

public class ProductStockService(
    IProductStockRepository repository,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider) : IProductStockService
{
    public async Task<Response<ProductStockDto>> AddProductStock(CreateProductStockDto request)
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
            if(!result.IsValid)
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

    public async Task<Response<ProductStockDto>> GetByProductCode(string productCode)
    {
        try
        {
            var productStock = await repository.GetByProductCodeAsync(productCode);
            if (productStock == null)
                return NotFoundStockResponse(productCode);

            return Response<ProductStockDto>.Success(productStock.ToDto());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Response<ProductStockDto> NotFoundStockResponse(string productCode)
        => Response<ProductStockDto>.Error(null,
            $"Nenhum estoque ativo foi encontrado para o produto com o código {productCode}",
            404
        );
}

