using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests.Invoice;
using Emitix.BillingService.DTOs.Response;

namespace Emitix.BillingService.Services;

public interface IBillingService
{
    Task<Response<InvoiceDto>> CreateInvoice(CreateInvoiceDto request);
    Task<Response<InvoiceDto>> PrintInvoice(PrintInvoiceDto request);
    Task<Response<InvoiceDto>> GetByNumberAndSeries(GetInvoiceDto request);
    Task<Response<InvoiceDto>> AlterInvoiceStatus(AlterInvoiceStatusDto request);
    Task<Response<List<InvoiceDto>>> GetAllInvoices();
}