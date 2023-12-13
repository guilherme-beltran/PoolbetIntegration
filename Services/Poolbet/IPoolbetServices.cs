using PoolbetIntegration.API.Features;
using PoolbetIntegration.API.Features.Login;

namespace PoolbetIntegration.API.Services.Poolbet;

public interface IPoolbetServices
{
    Task<Response> SendLogin(LoginRequest request);
}
