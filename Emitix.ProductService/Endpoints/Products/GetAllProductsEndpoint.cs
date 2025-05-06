using Emitix.ProductService.Common;
using Emitix.ProductService.DTOs.Responses;
using Emitix.ProductService.Services.Products;

namespace Emitix.ProductService.Endpoints.Products;

public class GetAllProductsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Products: Get All Products")
            .WithSummary("Returns all of the products.")
            .Produces<Response<List<ProductDto>>>(StatusCodes.Status200OK, "application/json");

    private static async Task<IResult> HandleAsync(IProductService service)
    {
        var result = await service.GetAllProducts();
        return TypedResults.Json(result, statusCode: result.Code);
    } 
}