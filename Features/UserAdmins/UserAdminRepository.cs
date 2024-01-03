using Microsoft.EntityFrameworkCore;
using PoolbetIntegration.API.Features.Contexts;

namespace PoolbetIntegration.API.Features.UserAdmins;

public class UserAdminRepository : IUserAdminRepository
{
    private readonly IAppDbContext _context;

    public UserAdminRepository(IAppDbContext appDbContext) 
        => _context = appDbContext;

    public async Task<IEnumerable<UserAdmin>> GetAllAsync(CancellationToken cancellationToken) 
        => await _context
                .UserAdmins
                .ToListAsync(cancellationToken);

    public async Task<UserAdmin?> GetByIdAsync(int id)
    {
        var userAdmin = await _context
                        .UserAdmins
                        .Where(x => x.UserAdminId == id)
                        .FirstOrDefaultAsync();

        return userAdmin;
    }

    public async Task<UserAdmin> GetByUsernameAndEmailAsync(string username, string email, CancellationToken cancellationToken)
    {
        var userAdmin = await _context
                        .UserAdmins
                        .Where(x => x.Username == username && x.Email == email)
                        .FirstOrDefaultAsync(cancellationToken);

        return userAdmin;
    }

    public async Task<UserAdmin?> GetBalanceAsync(string username, string email, CancellationToken cancellationToken)
    {
        var userAdmin = await _context
                        .UserAdmins
                        .Where(x => x.Username == username && x.Email == email)
                        .Select(u => new UserAdmin
                        {
                            Credit = u.Credit,
                            Username = u.Username,
                            Email = u.Email
                        })
                        .FirstOrDefaultAsync(cancellationToken);

        return userAdmin;
    }

    public async Task<bool> UpdateBalance(decimal value, string username, string email)
    {
        var updated = await _context
                .UserAdmins
                .Where(u => u.Username == username && u.Email == email)
                .ExecuteUpdateAsync(p =>
                    p.SetProperty(c => c.Credit, value));


        return updated != 0;
    }

    public async Task<bool> IsUserCorrect(string username, string email, int id, CancellationToken cancellationToken) 
        => await _context
                .UserAdmins
                .AnyAsync(u => u.Email.Equals(email) &&
                               u.UserAdminId.Equals(id),
                               cancellationToken);
}
