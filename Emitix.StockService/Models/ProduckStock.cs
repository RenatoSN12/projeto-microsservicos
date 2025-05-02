namespace Emitix.StockService.Models;

public class ProductStock
{
    public Guid Id { get; private set; }
    public string ProductCode { get; private set; } = string.Empty;
    public int Quantity { get; private set; }

    private ProductStock(){}

    private ProductStock(string productCode, int quantity)
    {
        Id = Guid.NewGuid();
        ProductCode = productCode;
        Quantity = quantity;
    }

    public static ProductStock Create(string productCode, int quantity)
        => new(productCode, quantity);
    
}