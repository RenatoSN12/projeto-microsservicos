using Emitix.ProductService.Common;
using Emitix.StockService.Common;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;

namespace Emitix.StockService.Services;

public interface IProductStockService
{
    Task<Response<ProductStockDto>> AddProductStock(CreateProductStockDto request);
    Task<Response<ProductStockDto>> UpdateProductStock(UpdateProductStockDto request);
    Task<Response<ProductStockDto>> GetByProductCode(string productCode);
}