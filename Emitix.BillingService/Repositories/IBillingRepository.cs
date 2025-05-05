using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.Models;

namespace Emitix.BillingService.Repositories;

public interface IBillingRepository
{
    Task CreateInvoiceAsync(Invoice invoice , CancellationToken cancellationToken = default);
    Task<Invoice?> GetInvoiceAndProductsByNumberAndSeriesAsync(GetInvoiceDto request, CancellationToken cancellationToken = default);
    void UpdateInvoice(Invoice invoice);
}