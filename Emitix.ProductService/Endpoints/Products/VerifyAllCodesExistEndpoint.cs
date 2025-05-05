using Emitix.ProductService.Common;
using Emitix.ProductService.Services.Products;

namespace Emitix.ProductService.Endpoints.Products;

public class VerifyAllCodesExistEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/verify-codes", HandleAsync)
            .WithName("Product: Verify All Codes Exist")
            .WithSummary("Verify all codes.")
            .Produces<Response<List<string>>>(StatusCodes.Status200OK, "text/plain");
    }

    private static async Task<IResult> HandleAsync(string[] codes, IProductService service)
    {
        var result = await service.VerifyAllCodesExist(codes);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}