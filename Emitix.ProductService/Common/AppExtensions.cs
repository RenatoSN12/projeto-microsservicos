using Emitix.ProductService.Endpoints;
using Emitix.ProductService.Endpoints.Products;

namespace Emitix.ProductService.Common;

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
        app.MapGroup("api/v1/products")
            .WithTags("Products")
            .MapEndpoint<GetProductByCodeEndpoint>()
            .MapEndpoint<GetAllProductsEndpoint>()
            .MapEndpoint<CreateProductEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app) where T : IEndpoint
    {
        T.Map(app);
        return app;
    }
}