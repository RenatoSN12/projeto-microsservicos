namespace Emitix.BillingService.DTOs.Requests;

public sealed record GetInvoiceDto(int InvoiceNumber, string InvoiceSeries);