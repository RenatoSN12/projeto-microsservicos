using Emitix.ProductService.Common;
using Emitix.ProductService.Common.Extensions;
using Emitix.ProductService.Data.UnitOfWork;
using Emitix.ProductService.DTOs.Requests;
using Emitix.ProductService.DTOs.Responses;
using Emitix.ProductService.Mappers;
using Emitix.ProductService.Repositories;
using FluentValidation;

namespace Emitix.ProductService.Services.Products;

public class ProductService(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider) 
    : IProductService
{
    public async Task<Response<ProductDto>> GetProductByCodeAsync(string productCode)
    {
        try
        {
            var product = await repository.GetByCodeAsync(productCode);
            if (product is null)
                return Response<ProductDto>.Error(null,
                    "Não foi encontrado um produto com o código informado.", 404);

            return Response<ProductDto>.Success(product.ToDto());
        }
        catch (Exception e)
        {
            return Response<ProductDto>.Error(null, e.Message, 500);
        }
    }

    public async Task<Response<ProductDto>> CreateProduct(CreateProductDto product)
    {
        try
        {
            var validator = serviceProvider.GetRequiredService<IValidator<CreateProductDto>>();
            var result = await validator.ValidateAsync(product);
            if(!result.IsValid)
                return Response<ProductDto>.Error(null, result.Errors.ToMessageString(), 400);

            var productEntity = product.ToEntity();
            await repository.CreateAsync(productEntity);
            await unitOfWork.CommitAsync();
            return Response<ProductDto>.Success(productEntity.ToDto(), code: 201);
        }
        catch (Exception e)
        {
            return Response<ProductDto>.Error(null, e.Message, 500);
        }
    }
}