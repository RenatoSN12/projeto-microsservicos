using Emitix.ProductService.Models;

namespace Emitix.ProductService.Repositories;

public interface IProductRepository
{
    Task CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<string>> GetProductsListByCodes(IEnumerable<string> codes, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllProducts(CancellationToken cancellationToken = default);
}