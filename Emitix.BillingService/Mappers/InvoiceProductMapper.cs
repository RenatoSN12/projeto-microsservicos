using Emitix.BillingService.DTOs.Requests.InvoiceProduct;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Models;

namespace Emitix.BillingService.Mappers;

public static class InvoiceProductMapper
{
    public static InvoiceProduct ToEntity(this CreateInvoiceProductDto invoiceProductDto, Guid invoiceId)
        => InvoiceProduct.Create(
            invoiceId,
            invoiceProductDto.ProductCode,
            invoiceProductDto.UnitPrice,
            invoiceProductDto.Quantity
        );

    public static InvoiceProductDto ToDto(this InvoiceProduct product)
        => new(product.Code, product.Quantity, product.UnitPrice, product.Subtotal);
}