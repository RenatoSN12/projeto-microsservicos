using Emitix.ProductService.Common;
using Emitix.ProductService.DTOs.Responses;
using Emitix.ProductService.Services.Products;

namespace Emitix.ProductService.Endpoints.Products;

public class GetProductByCodeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{code}", HandleAsync)
            .WithName("Product: Get Product By Code")
            .WithSummary("Retrieves product information by code.")
            .Produces<Response<ProductDto>>();
    }
    
    private static async Task<IResult> HandleAsync(string code, IProductService service)
    {
        var result = await service.GetProductByCode(code);
        return TypedResults.Json(result, statusCode: result.Code);
    }
    
}