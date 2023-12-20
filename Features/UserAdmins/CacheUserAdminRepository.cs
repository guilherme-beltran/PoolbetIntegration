using Microsoft.Extensions.Caching.Memory;
using PoolbetIntegration.API.Features.Transactions;
using PoolbetIntegration.API.Features.UnitOfWork;

namespace PoolbetIntegration.API.Features.UserAdmins;

public class CacheUserAdminRepository : ICacheUserAdminRepository
{
    private readonly IUserAdminRepository _decoratedUserAdmin;
    private readonly ITransactionRepository _decoratedTransaction;
    private readonly IMemoryCache _cache;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CacheUserAdminRepository> _logger;

    public CacheUserAdminRepository(IUserAdminRepository userAdminRepository, IMemoryCache cache, IUnitOfWork unitOfWork, ILogger<CacheUserAdminRepository> logger, ITransactionRepository decoratedTransaction)
    {
        _decoratedUserAdmin = userAdminRepository;
        _cache = cache;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _decoratedTransaction = decoratedTransaction;
    }

    public async Task<IEnumerable<UserAdmin>> GetAllAsync(CancellationToken cancellationToken)
    {
        string key = $"users";

        return await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decoratedUserAdmin.GetAllAsync(cancellationToken);
            });
    }

    public async Task<UserAdmin> GetByIdAsync(int id)
    {
        string key = $"user-{id}";

        return await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decoratedUserAdmin.GetByIdAsync(id);
            });
    }

    public async Task<UserAdmin> GetByUsernameAndEmailAsync(string username, string email, CancellationToken cancellationToken)
    {
        string key = $"user-{username}-{email}";

        return await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decoratedUserAdmin.GetByUsernameAndEmailAsync(username, email, cancellationToken);
            });
    }

    public async Task<UserAdmin> GetBalanceAsync(string username, string email, CancellationToken cancellationToken)
    {
        string key = $"user-{username}-{email}";
        return await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decoratedUserAdmin.GetBalanceAsync(username, email, cancellationToken);
            });
    }

    public async Task<TransactionResponse> UpdateBalance(decimal value, int type, string username, string email, string betuuiId, CancellationToken cancellationToken)
    {
        string key = $"user-{username}-{email}";
        var user = await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decoratedUserAdmin.GetByUsernameAndEmailAsync(username, email, cancellationToken);
            });

        user!.Clear();
        _logger.LogInformation($"Old credit: {user!.Credit}");

        if (value < 0)
        {
            value = Math.Abs(value);
        }

        if (type == 3)
        {
            try
            {
                var transaction = await _decoratedTransaction.GetByIdAsync(betuuiId);
                transaction.UpdateStatus(status: 3);
                var updated = await _decoratedTransaction.UpdateAsync(transaction);
                if (!updated)
                {
                    _logger.LogInformation($"Failed transaction.");
                    await _unitOfWork.Rollback();
                    return new TransactionResponse(status: false, user.Credit, "The transaction could not be saved");
                }

                await _unitOfWork.Commit(cancellationToken);

                return new TransactionResponse(status: true, user.Credit, $"Successful transaction.");
            }
            catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception($"{ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
        }

        user!.VerifyValue(value, type);
        if (!user.IsValid)
        {
            _logger.LogInformation($"Failed transaction.");
            await _unitOfWork.Rollback();
            return new TransactionResponse(status: false, user.Credit, "Amount is greater than current balance.");
        }

        _logger.LogInformation($"New credit: {user!.Credit}");

        try
        {
            _unitOfWork.BeginTransaction();
            _cache.Set(key, user);
            var updatedCreditUser = await _decoratedUserAdmin.UpdateBalance(user.Credit, username, email);

            if (!updatedCreditUser)
            {
                _logger.LogInformation($"Failed transaction.");
                await _unitOfWork.Rollback();
                return new TransactionResponse(status: false, user.Credit, "Balance too low.");
            }

            var transaction = Transaction.Create(transactionId: Guid.NewGuid(), status: 1, value: value, betUuiId: betuuiId);
            await _decoratedTransaction.Insert(transaction);

            _logger.LogInformation($"Successful transaction.");
            await _unitOfWork.Commit(cancellationToken);
            return new TransactionResponse(status: true, user.Credit, $"Successful transaction.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception($"{ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
