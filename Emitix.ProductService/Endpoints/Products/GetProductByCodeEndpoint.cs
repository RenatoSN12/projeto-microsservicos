using Emitix.ProductService.Common;
using Emitix.ProductService.DTOs;
using Emitix.ProductService.DTOs.Responses;
using Emitix.ProductService.Services;
using Emitix.ProductService.Services.Products;

namespace Emitix.ProductService.Endpoints.Products;

public class GetProductByCodeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetProduct)
            .WithName("Product: Get Product By Code")
            .WithSummary("Get a product by code")
            .Produces<Response<ProductDto>>();
    }
    
    private static async Task<IResult> GetProduct(IProductService service, string code)
    {
        var result = await service.GetProductByCodeAsync(code);
        return TypedResults.Json(result, statusCode: result.Code);
    }
    
}