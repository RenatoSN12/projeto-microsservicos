using Emitix.StockService.Data;
using Emitix.StockService.Models;
using Microsoft.EntityFrameworkCore;

namespace Emitix.StockService.Repositories;

public class ProductStockRepository(AppDbContext context) : IProductStockRepository
{
    public async Task AddAsync(ProductStock request, CancellationToken cancellationToken = default)
        => await context.Stocks.AddAsync(request, cancellationToken);

    public void Update(ProductStock request, CancellationToken cancellationToken = default)
        => context.Stocks.Update(request);
    
    public async Task<ProductStock?> GetByProductCodeAsync(string productCode, CancellationToken cancellationToken = default)
        => await context.Stocks.FirstOrDefaultAsync(x=> x.ProductCode == productCode, cancellationToken);
}