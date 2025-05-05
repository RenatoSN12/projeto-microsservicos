using Emitix.BillingService.Common.Enums;

namespace Emitix.BillingService.DTOs.Response;

public sealed record InvoiceDto(
    Guid Id,
    int Number,
    string Series,
    DateTime IssuedDate,
    decimal TotalAmount,
    EInvoiceStatus InvoiceStatus,
    IEnumerable<InvoiceProductDto> Products);