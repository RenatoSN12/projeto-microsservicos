using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Services;
using Emitix.BillingService.Services.Billing;

namespace Emitix.BillingService.Endpoints.Invoices;

public class CreateInvoiceEndpoint() : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Billing: Create Invoice")
            .WithSummary("Creates a new invoice with products.")
            .Produces<Response<InvoiceDto>>(StatusCodes.Status201Created, "application/json");
    }

    private static async Task<IResult> HandleAsync(CreateInvoiceDto request, IBillingService service)
    {
        var result = await service.CreateInvoiceAsync(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}