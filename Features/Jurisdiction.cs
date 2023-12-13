using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features;

public sealed class Jurisdiction
{
    [JsonProperty("id")]
    public int? Id;

    [JsonProperty("name")]
    public object? Name;
}
