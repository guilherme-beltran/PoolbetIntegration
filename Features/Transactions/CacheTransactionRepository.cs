using Microsoft.Extensions.Caching.Memory;

namespace PoolbetIntegration.API.Features.Transactions;

public class CacheTransactionRepository : ICacheTransactionRepository
{
    private readonly ITransactionRepository _decoratedTransaction;
    private readonly IMemoryCache _cache;

    public CacheTransactionRepository(ITransactionRepository decoratedTransaction, IMemoryCache cache)
    {
        _decoratedTransaction = decoratedTransaction;
        _cache = cache;
    }

    public async Task<Transaction> GetByBetIdAsync(string betId)
    {
        string key = $"transaction-{betId}";

        return await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decoratedTransaction.GetByBetIdAsync(betId);
            });
    }

    public async Task<bool> UpdateAsync(Transaction transaction)
    {
        string key = $"transaction-{transaction.BetUuiId}";
        var updated = await _decoratedTransaction.UpdateAsync(transaction);
        if (updated is false)
        {
            return false;
        }

        _cache.Set(key, transaction);
        return true;
    }

    public async Task Insert(Transaction transaction, CancellationToken cancellationToken)
    {
        string key = $"transaction-{transaction.BetUuiId}";
        
        await _decoratedTransaction.Insert(transaction);
        _cache.Set(key, transaction);
    }
}