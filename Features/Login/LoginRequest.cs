using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features.Login;

public sealed class LoginRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public int PartnerId { get; set; }
    public string Currency { get; set; }
}