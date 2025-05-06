using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Services.Billing;

namespace Emitix.BillingService.Endpoints.Invoices;

public class AlterInvoiceStatusEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/alter-status", HandleAsync)
            .WithName("Invoices: Alter Status")
            .WithSummary("Alter an invoice status.")
            .Produces<Response<InvoiceDto>>(StatusCodes.Status200OK, "application/json");
    }

    private static async Task<IResult> HandleAsync(AlterInvoiceStatusDto request,IBillingService service)
    {
        var result = await service.AlterInvoiceStatus(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}