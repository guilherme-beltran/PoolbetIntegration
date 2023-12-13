using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features.Login;

public sealed class LoginRequest
{
    public string Login { get; set; } = "clubguilherme";
    public string Password { get; set; } = "testeintegracao";
    public int PartnerId { get; set; } = 2011;
}