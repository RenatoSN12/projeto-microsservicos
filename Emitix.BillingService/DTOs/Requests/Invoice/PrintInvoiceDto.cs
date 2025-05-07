namespace Emitix.BillingService.DTOs.Requests.Invoice;

public sealed record PrintInvoiceDto(int InvoiceNumber, string InvoiceSeries);