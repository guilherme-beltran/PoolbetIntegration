using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features;

public sealed class Response
{
    [JsonProperty("user")]
    public User? User;

    [JsonProperty("token")]
    public string? Token;

    [JsonProperty("message")]
    public string? Message;
}
