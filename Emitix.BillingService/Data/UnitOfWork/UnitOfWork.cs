namespace Emitix.BillingService.Data.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}