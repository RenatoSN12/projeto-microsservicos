namespace Emitix.StockService.Endpoints.ProductStocks;

public class CreateProductStockEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost()
    }

    private static async Task HandleAsync()
    {
        
    }
}