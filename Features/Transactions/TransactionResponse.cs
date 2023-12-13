namespace PoolbetIntegration.API.Features.Transactions;

public class TransactionResponse
{
    public TransactionResponse(bool status, decimal credit, Guid id, string message)
    {
        Status = status;
        Credit = credit;
        Id = id;
        Message = message;
    }

    public bool Status { get; set; }
    public decimal Credit { get; set; }
    public Guid Id { get; set; }
    public string Message { get; set; }
}
