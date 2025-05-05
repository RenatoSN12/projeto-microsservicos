namespace Emitix.BillingService.Models;

public class InvoiceProduct
{

    private InvoiceProduct(){}

    private InvoiceProduct(Guid invoiceId, string code, decimal unitPrice, decimal quantity)
    {
        InvoiceId = invoiceId;
        Code = code;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal => Quantity * UnitPrice;

    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    
    public static InvoiceProduct Create(Guid invoiceId, string productCode, decimal unitPrice, decimal quantity)
        => new(invoiceId, productCode, unitPrice, quantity);
}