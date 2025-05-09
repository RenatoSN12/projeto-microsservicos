using Emitix.StockService.Common;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;

namespace Emitix.StockService.Services;

public interface IStockService
{
    Task<Response<ProductStockDto>> CreateProductStock(CreateProductStockDto request);
    Task<Response<List<ProductStockDto>>> UpdateProductStock(List<UpdateProductStockDto> request);
    Task<Response<ProductStockDto>> GetStockByProductCode(string productCode);
}