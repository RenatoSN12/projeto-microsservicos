namespace Emitix.StockService.DTOs.Responses;

public sealed record ProductStockDto(Guid Id, string ProductId, int Quantity, DateTime UpdatedAt);