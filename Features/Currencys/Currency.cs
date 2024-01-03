using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features.Currencys;

public sealed class Currency
{
    [JsonProperty("code")]
    public string Code;

    [JsonProperty("codein")]
    public string Codein;

    [JsonProperty("name")]
    public string Name;

    [JsonProperty("high")]
    public string High;

    [JsonProperty("low")]
    public string Low;

    [JsonProperty("varBid")]
    public string VarBid;

    [JsonProperty("pctChange")]
    public string PctChange;

    [JsonProperty("bid")]
    public string Bid;

    [JsonProperty("ask")]
    public string Ask;

    [JsonProperty("timestamp")]
    public string Timestamp;

    [JsonProperty("create_date")]
    public string CreateDate;
}
