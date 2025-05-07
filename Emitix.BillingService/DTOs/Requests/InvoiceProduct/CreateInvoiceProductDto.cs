namespace Emitix.BillingService.DTOs.Requests.InvoiceProduct;

public sealed record CreateInvoiceProductDto(string ProductCode, decimal Quantity, decimal UnitPrice);