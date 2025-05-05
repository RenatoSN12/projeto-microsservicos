using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.DTOs.Response;

namespace Emitix.BillingService.Services.Billing;

public interface IBillingService
{
    Task<Response<InvoiceDto>> CreateInvoiceAsync(CreateInvoiceDto request);
    Task<Response<InvoiceDto>> PrintInvoiceAsync(PrintInvoiceDto request);
    Task<Response<InvoiceDto>> GetByNumberAndSeriesAsync(GetInvoiceDto request);
    Task<Response<List<InvoiceDto>>> GetAllInvoices();
}