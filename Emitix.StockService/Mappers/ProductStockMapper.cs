using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;
using Emitix.StockService.Models;
using ProductStockDto = Emitix.StockService.DTOs.Responses.ProductStockDto;

namespace Emitix.StockService.Mappers;

public static class ProductStockMapper
{
    public static ProductStock ToEntity(this CreateProductStockDto dto) 
        => ProductStock.Create(dto.ProductId, dto.Quantity);
    
    public static ProductStockDto ToDto(this ProductStock productStock)
        => new(
            productStock.Id,
            productStock.ProductCode,
            productStock.Quantity,
            productStock.UpdatedAt
        );
}