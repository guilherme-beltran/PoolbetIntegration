using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using PoolbetIntegration.API.Features.Contexts;
using PoolbetIntegration.API.Features.Transactions;
using PoolbetIntegration.API.Features.UserAdmins;

namespace PoolbetIntegration.API.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext() {}
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserAdmin> UserAdmins { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfiguration(new UserAdminMap());
        modelBuilder.ApplyConfiguration(new TransactionMap());

        base.OnModelCreating(modelBuilder);
    }
}
