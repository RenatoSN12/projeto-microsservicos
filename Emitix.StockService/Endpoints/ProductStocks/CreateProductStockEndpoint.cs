using Emitix.StockService.Common;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.DTOs.Responses;
using Emitix.StockService.Services;

namespace Emitix.StockService.Endpoints.ProductStocks;

public class CreateProductStockEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Stock: Create Product Stock")
            .WithSummary("Creates a stock for a product.")
            .Produces<Response<ProductStockDto>>(StatusCodes.Status201Created, "application/json");
    }

    private static async Task<IResult> HandleAsync(CreateProductStockDto request,IProductStockService service)
    {
        var result = await service.CreateProductStock(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}