namespace PoolbetIntegration.API.Features.Transactions;

public interface ITransactionRepository
{
    Task<Transaction> GetByBetIdAsync(string betuuiId);
    Task<bool> UpdateAsync(Transaction transaction);
    Task Insert(Transaction transaction);
}
