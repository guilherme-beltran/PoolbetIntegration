using Microsoft.EntityFrameworkCore.Storage;
using PoolbetIntegration.API.Data;

namespace PoolbetIntegration.API.Features.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
        => _context = context;

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            _transaction?.Commit();
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public async Task Rollback()
    {
        _transaction?.RollbackAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
