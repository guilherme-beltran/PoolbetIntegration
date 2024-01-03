using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features.Currencys;

public class CurrencyResponse
{
    [JsonIgnore]
    public decimal Value { get; set; }

    [JsonProperty("status")]
    public int? Status;

    [JsonProperty("code")]
    public string? Code;

    [JsonProperty("message")]
    public string? Message;
}