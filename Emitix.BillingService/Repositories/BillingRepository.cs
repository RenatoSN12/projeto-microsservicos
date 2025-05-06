using Emitix.BillingService.Common.Enums;
using Emitix.BillingService.Data;
using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.Models;
using Microsoft.EntityFrameworkCore;

namespace Emitix.BillingService.Repositories;

public class BillingRepository(AppDbContext context) : IBillingRepository
{
    public async Task CreateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default)
        => await context.Invoices.AddAsync(invoice, cancellationToken);

    public async Task<Invoice?> GetInvoiceWithProductsByNumberAndSeriesAsync(GetInvoiceDto request,
        CancellationToken cancellationToken = default)
        => await context.Invoices
            .AsNoTracking()
            .Include(i => i.Products)
            .FirstOrDefaultAsync(x =>
                    x.Number == request.InvoiceNumber &&
                    x.Series == request.InvoiceSeries,
                cancellationToken
            );

    public async Task<List<Invoice>> GetInvoicesWithProductsAsync(CancellationToken cancellationToken = default)
        => await context.Invoices
            .AsNoTracking()
            .Where(x=> x.InvoiceStatus == EInvoiceStatus.Open)
            .Include(i => i.Products)
            .OrderByDescending(x=> x.Number)
            .ToListAsync(cancellationToken);

    public void Update(Invoice invoice)
        => context.Invoices.Update(invoice);
}