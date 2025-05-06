using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.Models;

namespace Emitix.StockService.Repositories;

public interface IProductStockRepository
{
    Task AddAsync(ProductStock request, CancellationToken cancellationToken = default);
    void Update(ProductStock request, CancellationToken cancellationToken = default);
    Task<ProductStock?> GetByProductCodeAsync(string productCode, CancellationToken cancellationToken = default);
    Task<List<ProductStock>> GetByListProductCodesAsync(string[] productCodes, CancellationToken cancellationToken = default);
}