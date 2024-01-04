
using Microsoft.EntityFrameworkCore;
using PoolbetIntegration.API.Data;

namespace PoolbetIntegration.API.Features.Transactions;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
        => _context = context;

    public async Task<Transaction?> GetByBetIdAsync(string betuuiId)
    {
        var transaction = await _context.Transactions.Where(t => t.BetUuiId == betuuiId).FirstOrDefaultAsync();

        return transaction;
    }

    public async Task Insert(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task<bool> UpdateAsync(Transaction transaction)
    {
        var updated = await _context.Transactions.Where(t => t.BetUuiId == transaction.BetUuiId).ExecuteUpdateAsync(x => x.SetProperty(x => x.Status, transaction.Status));

        return updated != 0;
    }
}
