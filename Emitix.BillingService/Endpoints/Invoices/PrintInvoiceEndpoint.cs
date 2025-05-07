using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests.Invoice;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Services;

namespace Emitix.BillingService.Endpoints.Invoices;

public class PrintInvoiceEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/print", HandleAsync)
            .WithName("Invoice: Print an invoice")
            .WithSummary("Print an invoice")
            .Produces<Response<InvoiceDto>>(StatusCodes.Status200OK, "application/json");
    }

    private static async Task<IResult> HandleAsync(PrintInvoiceDto request,IBillingService service)
    {
        var result = await service.PrintInvoice(request);
        return TypedResults.Json(result, statusCode: result.Code);
    }
}