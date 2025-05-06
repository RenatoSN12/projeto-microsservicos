namespace Emitix.StockService.DTOs.Requests;

public sealed record ProductStockRequest(string ProductCode, int Quantity);