using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.Services.Billing;

namespace Emitix.BillingService.Endpoints.Invoices;

public class PrintInvoiceEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/print", HandleAsync);
    }

    private static async Task<IResult> HandleAsync(PrintInvoiceDto request,IBillingService service)
    {
        var result = await service.PrintInvoiceAsync(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}