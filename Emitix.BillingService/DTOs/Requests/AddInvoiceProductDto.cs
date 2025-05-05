namespace Emitix.BillingService.DTOs.Requests;

public sealed record AddInvoiceProductDto(
    string ProductCode,
    int InvoiceNumber,
    string InvoiceSeries,
    decimal UnitPrice,
    int Quantity);