using Emitix.StockService.Endpoints;
using Emitix.StockService.Endpoints.ProductStocks;

namespace Emitix.StockService.Common;

public static class AppExtensions
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapSwagger().RequireAuthorization();
    }

    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("api/v1/stock")
            .WithTags("Stocks")
            .MapEndpoint<CreateProductStockEndpoint>()
            .MapEndpoint<UpdateProductStockEndpoint>()
            .MapEndpoint<VerifyStockAvailabilityEndpoint>()
            .MapEndpoint<GetStockByProductCodeEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app) where T : IEndpoint
    {
        T.Map(app);
        return app;
    }
}