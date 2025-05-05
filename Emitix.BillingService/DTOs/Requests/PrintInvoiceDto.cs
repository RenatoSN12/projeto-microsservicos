namespace Emitix.BillingService.DTOs.Requests;

public sealed record PrintInvoiceDto(int InvoiceNumber, string InvoiceSeries);