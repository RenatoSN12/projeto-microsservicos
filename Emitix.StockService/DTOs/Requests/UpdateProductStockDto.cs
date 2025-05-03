using Emitix.StockService.Common.Enums;

namespace Emitix.StockService.DTOs.Requests;

public sealed record UpdateProductStockDto(string ProductCode, int Quantity, EMovementType MovementType);