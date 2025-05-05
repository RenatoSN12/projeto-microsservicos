using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Models;

namespace Emitix.BillingService.Mappers;

public static class InvoiceMapper
{
    public static Invoice ToEntity(this CreateInvoiceDto invoiceDto)
    {
        var invoice =  Invoice.Create(invoiceDto.Number, invoiceDto.Series);
        
        var invoiceProducts = invoiceDto.Products
            .Select(x=> x.ToEntity(invoice.Id))
            .ToList();
        
        invoice.AddProducts(invoiceProducts);
        
        return invoice;
    }
    
    public static InvoiceDto ToDto(this Invoice entity)
        => new(
            entity.Number,
            entity.Series,
            entity.CreatedAt,
            entity.Products.Sum(v => v.UnitPrice * v.Quantity),
            entity.InvoiceStatus,
            entity.Products.Select(x=> x.ToDto()));
}