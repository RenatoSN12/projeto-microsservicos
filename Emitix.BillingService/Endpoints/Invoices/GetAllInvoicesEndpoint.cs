using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests.Invoice;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Services;

namespace Emitix.BillingService.Endpoints.Invoices;

public class GetAllInvoicesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Billing: Get All Invoices")
            .WithSummary("Returns all invoices")
            .Produces<Response<InvoiceDto>>(StatusCodes.Status200OK, "application/json")
            .Produces<Response<List<InvoiceDto>>>(StatusCodes.Status200OK, "application/json");

    private static async Task<IResult> HandleAsync([AsParameters] GetInvoiceDto filters, IBillingService service)
    {
        if (filters.InvoiceNumber != null && !string.IsNullOrWhiteSpace(filters.InvoiceSeries))
        {
            var invoice = await service.GetByNumberAndSeries(filters);
            return TypedResults.Json(invoice, statusCode: invoice.Code);
        }
        
        var invoices = await service.GetAllInvoices();
        return TypedResults.Json(invoices, statusCode: invoices.Code);
    } 
}