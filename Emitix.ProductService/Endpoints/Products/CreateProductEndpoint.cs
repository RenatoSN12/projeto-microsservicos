using Emitix.ProductService.Common;
using Emitix.ProductService.DTOs.Requests;
using Emitix.ProductService.DTOs.Responses;
using Emitix.ProductService.Services.Products;

namespace Emitix.ProductService.Endpoints.Products;

public class CreateProductEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Product: Create Product")
            .WithSummary("Creates a new product")
            .Produces<Response<ProductDto>>(StatusCodes.Status201Created, "application/json");

    private static async Task<IResult> HandleAsync(CreateProductDto request, IProductService service)
    {
        var result = await service.CreateProduct(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}