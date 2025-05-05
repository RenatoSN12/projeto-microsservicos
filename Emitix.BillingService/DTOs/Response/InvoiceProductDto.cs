namespace Emitix.BillingService.DTOs.Response;

public sealed record InvoiceProductDto(string ProductCode, decimal Quantity, decimal UnitPrice, decimal Subtotal);