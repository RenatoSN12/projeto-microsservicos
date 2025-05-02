using Emitix.ProductService.Data;
using Emitix.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace Emitix.ProductService.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task CreateAsync(Product product, CancellationToken cancellationToken = default)
        => await context.Products.AddAsync(product, cancellationToken);

    public async Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken)
        => await context.Products.AsNoTracking().FirstOrDefaultAsync(x=>x.Code == code, cancellationToken);
}