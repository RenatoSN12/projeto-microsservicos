namespace Emitix.BillingService.DTOs.Requests;

public sealed record CreateInvoiceProductDto(string ProductCode, decimal Quantity, decimal UnitPrice);