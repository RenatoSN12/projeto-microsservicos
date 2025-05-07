namespace Emitix.StockService.DTOs.Responses;

public sealed record ProductStockDto(Guid Id, string ProductCode, int Quantity, DateTime UpdatedAt);