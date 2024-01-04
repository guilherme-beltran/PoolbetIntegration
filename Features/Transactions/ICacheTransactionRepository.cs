namespace PoolbetIntegration.API.Features.Transactions;

public interface ICacheTransactionRepository
{
    Task<Transaction> GetByBetIdAsync(string betId);
    Task<bool> UpdateAsync(Transaction transaction);
    Task Insert(Transaction transaction, CancellationToken cancellationToken);
}
