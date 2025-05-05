using Emitix.StockService.Common;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;

namespace Emitix.StockService.Services;

public interface IProductStockService
{
    Task<Response<ProductStockDto>> CreateProductStock(CreateProductStockDto request);
    Task<Response<ProductStockDto>> UpdateProductStock(UpdateProductStockDto request);
    Task<Response<ProductStockDto>> GetStockByProductCode(string productCode);
}