using Emitix.StockService.Common;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.Services;

namespace Emitix.StockService.Endpoints.ProductStocks;

public class VerifyStockAvailabilityEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/verify", HandleAsync)
            .WithName("Stock: Verify Availability")
            .WithSummary("Verifys stock availability.")
            .Produces<Response<bool>>(StatusCodes.Status200OK, "application/json");
    }

    private static async Task<IResult> HandleAsync(List<ProductStockRequestDto> request, IStockService service)
    {
        var result = await service.VerifyStockAvailability(request);
        return TypedResults.Json(result, statusCode:result.Code);
    }
}