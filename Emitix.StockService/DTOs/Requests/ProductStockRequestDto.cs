namespace Emitix.StockService.DTOs.Requests;

public sealed record ProductStockRequestDto(string ProductCode, int Quantity);