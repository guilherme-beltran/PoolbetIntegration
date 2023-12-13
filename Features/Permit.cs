using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features;

public sealed class Permit
{
    [JsonProperty("casinoDefaultPermits")]
    public string? CasinoDefaultPermits;

    [JsonProperty("nationalDefaultPermits")]
    public string? NationalDefaultPermits;

    [JsonProperty("reginioalDefaultPermits")]
    public string? ReginioalDefaultPermits;

    [JsonProperty("distritoDefaultPermits")]
    public string? DistritoDefaultPermits;

    [JsonProperty("localDefaultPermits")]
    public string? LocalDefaultPermits;

    [JsonProperty("jurisdictionsDefaultPermits")]
    public string? JurisdictionsDefaultPermits;

    [JsonProperty("clubDefaultPermits")]
    public string? ClubDefaultPermits;

    [JsonProperty("name")]
    public string? Name;

    [JsonProperty("description")]
    public string? Description;

    [JsonProperty("id")]
    public int? Id;
}
