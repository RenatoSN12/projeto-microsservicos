using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests.Invoice;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Services;

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
        var result = await service.CreateInvoice(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}