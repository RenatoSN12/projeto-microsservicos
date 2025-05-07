using Emitix.BillingService.DTOs.Requests.Invoice;
using Emitix.BillingService.Models;

namespace Emitix.BillingService.Repositories;

public interface IBillingRepository
{
    Task CreateInvoiceAsync(Invoice invoice , CancellationToken cancellationToken = default);
    Task<Invoice?> GetInvoiceWithProductsByNumberAndSeriesAsync(GetInvoiceDto request, CancellationToken cancellationToken = default);
    Task<List<Invoice>> GetInvoicesWithProductsAsync(CancellationToken cancellationToken = default);
    void Update(Invoice invoice);
}