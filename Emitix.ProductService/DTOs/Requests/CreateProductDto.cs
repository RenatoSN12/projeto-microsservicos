namespace Emitix.ProductService.DTOs.Requests;

public sealed record CreateProductDto(string Code, string? Description, decimal Price);