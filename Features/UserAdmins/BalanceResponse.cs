namespace PoolbetIntegration.API.Features.UserAdmins;

public class BalanceResponse
{
    public BalanceResponse(bool status,
                           decimal credit,
                           string currency,
                           string? error = null)
    {
        Status = status;
        Credit = credit;
        Currency = currency;
        Error = error;
    }
    public BalanceResponse(bool status,
                           decimal credit,
                           string? error = null,
                           string? key = null,
                           string? currency = null)
    {
        Status = status;
        Credit = credit;
        Error = error;
        Key = key;
        Currency = currency;
    }

    public bool Status { get; set; }
    public decimal Credit { get; set; }
    public string? Error { get; set; }
    public string? Key { get; set; }
    public string? Currency { get; set; }
}
