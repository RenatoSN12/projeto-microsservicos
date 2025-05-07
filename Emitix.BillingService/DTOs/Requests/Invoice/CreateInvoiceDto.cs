using Emitix.BillingService.DTOs.Requests.InvoiceProduct;

namespace Emitix.BillingService.DTOs.Requests.Invoice;

public sealed record CreateInvoiceDto(
    int Number,
    string Series,
    List<CreateInvoiceProductDto> Products);