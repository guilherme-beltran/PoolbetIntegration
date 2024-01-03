using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features.Currencys;

public class CurrencyError
{
    [JsonProperty("code")]
    public int Code;

    [JsonProperty("status")]
    public int Status;

    [JsonProperty("message")]
    public string Message;
}
