namespace Emitix.BillingService.DTOs.Requests;

public sealed record CreateInvoiceDto(
    int Number,
    string Series,
    List<CreateInvoiceProductDto> Products);