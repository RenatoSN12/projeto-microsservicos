using Emitix.ProductService.Common.Enums;

namespace Emitix.ProductService.DTOs.Responses;

public sealed record ProductDto(string Code, string? Description, decimal Price, EProductStatus Status);
