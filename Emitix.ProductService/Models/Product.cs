using Emitix.ProductService.Common.Enums;

namespace Emitix.ProductService.Models;

public class Product
{
    private Product() { }

    private Product(Guid id, string code, decimal price, string? description)
    {
        Id = id;
        Code = code;
        Price = price;
        Description = description;
        Status = EProductStatus.Active;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public EProductStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static Product Create(string code, decimal price, string? description = null) =>
        new(Guid.NewGuid(), code, price, description);
}