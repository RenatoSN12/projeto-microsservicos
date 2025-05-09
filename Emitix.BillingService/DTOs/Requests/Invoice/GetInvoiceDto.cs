using Microsoft.AspNetCore.Mvc;

namespace Emitix.BillingService.DTOs.Requests.Invoice;

public sealed record GetInvoiceDto(
    [FromQuery(Name = "invoiceNumber")] int? InvoiceNumber,
    [FromQuery(Name = "invoiceSeries")] string? InvoiceSeries);