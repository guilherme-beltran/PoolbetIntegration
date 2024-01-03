namespace PoolbetIntegration.API.Features.UserAdmins;

public interface IBalanceHandler
{
    Task<BalanceResponse> Handle(BalanceRequest request, CancellationToken cancellationToken);
}
