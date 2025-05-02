namespace Emitix.ProductService.DTOs.Requests;

public record CreateProductDto(string Code, string? Description, decimal Price);