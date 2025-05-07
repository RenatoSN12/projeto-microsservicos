using Emitix.ProductService.Common;
using Emitix.ProductService.DTOs.Requests;
using Emitix.ProductService.DTOs.Responses;

namespace Emitix.ProductService.Services.Products;

public interface IProductService
{
    Task<Response<ProductDto>> GetProductByCode(string productCode);
    Task<Response<ProductDto>> CreateProduct(CreateProductDto product);
    Task<Response<List<ProductDto>>> GetAllProducts();
}