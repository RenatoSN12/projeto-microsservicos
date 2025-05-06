using Emitix.StockService.Common;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;
using Emitix.StockService.Services;

namespace Emitix.StockService.Endpoints.ProductStocks;

public class UpdateProductStockEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/", HandleAsync)
            .WithName("Stock: Update Product Stock")
            .WithSummary("Updates the stock quantity for a product.")
            .Produces<Response<ProductStockDto>>(StatusCodes.Status200OK, "application/json");
    }

    private static async Task<IResult> HandleAsync(UpdateProductStockDto request,IStockService service)
    {
        var result = await service.UpdateProductStock(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}