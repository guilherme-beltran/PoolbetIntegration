using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features;

public sealed class AccountSummary
{
    [JsonProperty("accountAccess")]
    public object? AccountAccess;

    [JsonProperty("ip")]
    public object? Ip;

    [JsonProperty("registrationDateTime")]
    public object? RegistrationDateTime;

    [JsonProperty("lastAccess")]
    public object? LastAccess;

    [JsonProperty("nrLogin")]
    public int? NrLogin;

    [JsonProperty("accountBalance")]
    public double? AccountBalance;

    [JsonProperty("totalDeposits")]
    public double? TotalDeposits;

    [JsonProperty("totalWithdrawals")]
    public double? TotalWithdrawals;

    [JsonProperty("last3Access")]
    public List<object?>? Last3Access;

    [JsonProperty("last3IP")]
    public List<object?>? Last3IP;
}
