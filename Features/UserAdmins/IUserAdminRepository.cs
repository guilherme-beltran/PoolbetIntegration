namespace PoolbetIntegration.API.Features.UserAdmins;

public interface IUserAdminRepository
{
    Task<IEnumerable<UserAdmin>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserAdmin> GetByIdAsync(int id);
    Task<UserAdmin> GetByUsernameAndEmailAsync(string username, string email, CancellationToken cancellationToken);
    Task<UserAdmin> GetBalanceAsync(string username, string email, CancellationToken cancellationToken);
    Task<bool> UpdateBalance(decimal value, string username, string email);
}
