using Microsoft.EntityFrameworkCore;
using PoolbetIntegration.API.Data;
using PoolbetIntegration.API.Features.Contexts;
using PoolbetIntegration.API.Features.Currencys;
using PoolbetIntegration.API.Features.Transactions;
using PoolbetIntegration.API.Features.UnitOfWork;
using PoolbetIntegration.API.Features.UserAdmins;
using PoolbetIntegration.API.Services.Poolbet;

namespace PoolbetIntegration.API.Extensions.Services;

public static class ServicesExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("Database") ?? throw new Exception($"Ñenhuma conexão foi definida.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
        services.AddScoped<IAppDbContext, AppDbContext>();

    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IPoolbetServices, PoolbetServices>();

        services.AddScoped<IUserAdminRepository, UserAdminRepository>();
        services.AddScoped<ICacheUserAdminRepository, CacheUserAdminRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBalanceHandler, BalanceHandler>();
        services.AddScoped<ICurrencyServices, CurrencyServices>();
        services.AddScoped<ICurrencyQuotes, CurrencyQuotes>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });


        services.AddClients();
    }

    public static void AddClients(this IServiceCollection services)
    {
        services.AddHttpClient("poolbet", (client) =>
        {
            client.BaseAddress = new Uri("https://localhost:5001");
        });

        services.AddHttpClient("economia", (client) =>
        {
            client.BaseAddress = new Uri("https://economia.awesomeapi.com.br");
        });
    }
}
