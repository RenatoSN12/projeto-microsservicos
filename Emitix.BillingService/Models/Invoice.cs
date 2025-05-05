using System.Collections;
using System.Collections.ObjectModel;
using Emitix.BillingService.Common.Enums;
using Emitix.BillingService.Exceptions;

namespace Emitix.BillingService.Models;

public class Invoice
{

    private Invoice() { }

    private Invoice(int number, string series)
    {
        Id = Guid.NewGuid();
        Number = number;
        Series = series;
        InvoiceStatus = EInvoiceStatus.Open;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; private set; }
    public int Number { get; private set; }
    public string Series { get; private set; } = string.Empty;
    public EInvoiceStatus InvoiceStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<InvoiceProduct> _products = [];
    public IReadOnlyCollection<InvoiceProduct> Products => _products;
    public static Invoice Create(int number, string series)
        => new(number, series);

    public void AddProducts(IEnumerable<InvoiceProduct> products)
    {
        if (InvoiceStatus != EInvoiceStatus.Open)
            throw new InvoiceStatusException("Não é possível adicionar produtos à uma nota fiscal fechada.");
        
        _products.AddRange(products);
    }
}