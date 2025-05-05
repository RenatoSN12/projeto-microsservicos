namespace Emitix.BillingService.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}