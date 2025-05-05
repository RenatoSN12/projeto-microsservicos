using Emitix.StockService.Common;
using Emitix.StockService.DTOs.Responses;
using Emitix.StockService.Services;

namespace Emitix.StockService.Endpoints.ProductStocks;

public class GetStockByProductCodeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Stock: Get Product Stock")
            .WithSummary("Retrieves the stock information for a product.")
            .Produces<Response<ProductStockDto>>(StatusCodes.Status200OK, "application/json");
    }

    private static async Task<IResult> HandleAsync(string productCode,IProductStockService service)
    {
        var result = await service.GetStockByProductCode(productCode);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}