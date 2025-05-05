using Emitix.BillingService.Endpoints;
using Emitix.BillingService.Endpoints.Invoices;

namespace Emitix.BillingService.Common;

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
        app.MapGroup("api/v1/billing")
            .WithTags("Products")
            .MapEndpoint<CreateInvoiceEndpoint>()
            .MapEndpoint<PrintInvoiceEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app) where T : IEndpoint
    {
        T.Map(app);
        return app;
    }
}