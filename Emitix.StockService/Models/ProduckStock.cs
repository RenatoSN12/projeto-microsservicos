using Emitix.StockService.Common.Enums;
using Emitix.StockService.Exceptions;

namespace Emitix.StockService.Models;

public class ProductStock
{
    public Guid Id { get; private set; }
    public string ProductCode { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    private ProductStock(){}
    private ProductStock(string productCode, int quantity)
    {
        Id = Guid.NewGuid();
        ProductCode = productCode;
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public static ProductStock Create(string productCode, int quantity)
        => new(productCode, quantity);

    public void Update(int quantity, EMovementType movementType)
    {
        switch (movementType)
        {
            case EMovementType.Outbound:
                if (Quantity - quantity < 0)
                    throw new InsufficientStockException(ProductCode);
                
                Quantity -= quantity;
                break;
            case EMovementType.Inbound:
                Quantity += quantity;
                break;
            default:
                throw new InvalidMovementType();
        }

        UpdatedAt = DateTime.UtcNow;
    }
}