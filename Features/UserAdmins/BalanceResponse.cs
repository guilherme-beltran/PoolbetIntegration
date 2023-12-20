namespace PoolbetIntegration.API.Features.UserAdmins;

public class BalanceResponse
{
    public BalanceResponse(bool status, decimal credit, string? error = null)
    {
        Status = status;
        Credit = credit;
        Error = error;
    }
    public BalanceResponse(bool status, decimal credit, string? error = null, string? key = null)
    {
        Status = status;
        Credit = credit;
        Error = error;
        Key = key;
    }

    public bool Status { get; set; }
    public decimal Credit { get; set; }
    public string? Error { get; set; }
    public string? Key { get; set; }
}
