using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features.Currencys;

public class CurrencyError
{
    [JsonProperty("code")]
    public string Code;

    [JsonProperty("status")]
    public int Status;

    [JsonProperty("message")]
    public string Message;
}
