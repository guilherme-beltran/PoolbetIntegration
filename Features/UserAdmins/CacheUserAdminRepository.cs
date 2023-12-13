using Microsoft.Extensions.Caching.Memory;
using PoolbetIntegration.API.Features.Transactions;
using PoolbetIntegration.API.Features.UnitOfWork;

namespace PoolbetIntegration.API.Features.UserAdmins;

public class CacheUserAdminRepository : ICacheUserAdminRepository
{
    private readonly IUserAdminRepository _decorated;
    private readonly IMemoryCache _cache;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CacheUserAdminRepository> _logger;

    public CacheUserAdminRepository(IUserAdminRepository userAdminRepository, IMemoryCache cache, IUnitOfWork unitOfWork, ILogger<CacheUserAdminRepository> logger)
    {
        _decorated = userAdminRepository;
        _cache = cache;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<UserAdmin>> GetAllAsync(CancellationToken cancellationToken)
    {
        string key = $"users";

        return await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decorated.GetAllAsync(cancellationToken);
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
                return _decorated.GetByIdAsync(id);
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
                return _decorated.GetByUsernameAndEmailAsync(username, email, cancellationToken);
            });
    }

    public async Task<TransactionResponse> UpdateBalance(decimal value, int type, string username, string email, CancellationToken cancellationToken)
    {
        string key = $"user-{username}-{email}";
        var user = await _cache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(240));
                return _decorated.GetByUsernameAndEmailAsync(username, email, cancellationToken);
            });

        user!.Clear();
        _logger.LogInformation($"Old credit: {user!.Credit}");

        if (value < 0)
        {
            value = Math.Abs(value);
        }

        user!.VerifyValue(value, type);
        if (!user.IsValid)
        {
            _logger.LogInformation($"Failed transaction.");
            await _unitOfWork.Rollback();
            return new TransactionResponse(status: false, user.Credit, Guid.Empty, "Amount is greater than current balance.");
        }

        _logger.LogInformation($"New credit: {user!.Credit}");

        try
        {
            _unitOfWork.BeginTransaction();
            _cache.Set(key, user);
            var updated = await _decorated.UpdateBalance(user.Credit, username, email);

            if (!updated)
            {
                _logger.LogInformation($"Failed transaction.");
                await _unitOfWork.Rollback();
                return new TransactionResponse(status: false, user.Credit, Guid.Empty, "Balance too low.");
            }

            _logger.LogInformation($"Successful transaction.");
            await _unitOfWork.Commit(cancellationToken);
            return new TransactionResponse(status: true, user.Credit, Guid.NewGuid(), $"Successful transaction.");
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
