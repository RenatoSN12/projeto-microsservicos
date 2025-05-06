namespace Emitix.BillingService.DTOs.Requests;

public sealed record StockAvailabilityRequest(string ProductCode, int Quantity);