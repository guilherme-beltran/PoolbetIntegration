using Microsoft.EntityFrameworkCore;
using PoolbetIntegration.API.Features.UserAdmins;

namespace PoolbetIntegration.API.Features.Contexts;

public interface IAppDbContext
{
    public DbSet<UserAdmin> UserAdmins { get; set; }
}
