using Emitix.ProductService.DTOs.Requests;
using Emitix.ProductService.DTOs.Responses;
using Emitix.ProductService.Models;

namespace Emitix.ProductService.Mappers;

public static class ProductMapper
{
    public static Product ToEntity(this CreateProductDto productDto) 
        => Product.Create(productDto.Code, productDto.Price, productDto.Description);
    
    public static ProductDto ToDto(this Product product) =>
        new(product.Code, product.Description, product.Price, product.Status);
}