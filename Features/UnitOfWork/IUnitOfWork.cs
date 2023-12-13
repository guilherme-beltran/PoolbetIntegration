namespace PoolbetIntegration.API.Features.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task Commit(CancellationToken cancellationToken);
    Task Rollback();
}
