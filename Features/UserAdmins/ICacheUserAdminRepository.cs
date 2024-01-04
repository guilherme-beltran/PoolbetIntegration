using PoolbetIntegration.API.Features.Transactions;

namespace PoolbetIntegration.API.Features.UserAdmins;

public interface ICacheUserAdminRepository
{
    Task<IEnumerable<UserAdmin>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserAdmin> GetByIdAsync(int id);
    Task<UserAdmin> GetByUsernameAndEmailAsync(string username, string email, CancellationToken cancellationToken);
    Task<UserAdmin> GetBalanceAsync(string username, string email, CancellationToken cancellationToken);
    Task<TransactionResponse> UpdateBalance(decimal value, int type, string username, string email, string betId, string currency, CancellationToken cancellationToken);
    Task<bool> UpdateBalance(UserAdmin userAdmin, CancellationToken cancellationToken);
    Task<CheckUserResponse> IsUserCorrect(string username, string email, int id, CancellationToken cancellationToken);
}
