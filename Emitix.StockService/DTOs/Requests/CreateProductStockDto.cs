namespace Emitix.StockService.DTOs.Requests;

public sealed record CreateProductStockDto(string ProductId, int Quantity);