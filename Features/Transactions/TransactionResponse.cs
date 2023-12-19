namespace PoolbetIntegration.API.Features.Transactions;

public class TransactionResponse
{
    public TransactionResponse(bool status, decimal credit, string message)
    {
        Status = status;
        Credit = credit;
        Message = message;
    }

    public bool Status { get; set; }
    public decimal Credit { get; set; }
    public string Message { get; set; }
}
