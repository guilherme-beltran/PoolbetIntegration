namespace PoolbetIntegration.API.Features.Transactions;

public interface ITransactionRepository
{
    Task<Transaction> GetByIdAsync(string betuuiId);
    Task<bool> UpdateAsync(Transaction transaction);
    Task Insert(Transaction transaction);
}
