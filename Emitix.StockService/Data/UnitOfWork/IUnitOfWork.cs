namespace Emitix.StockService.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}