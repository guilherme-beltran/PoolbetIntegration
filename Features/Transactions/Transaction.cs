namespace PoolbetIntegration.API.Features.Transactions;

public class Transaction 
{
    public Guid TransactionId { get; set; }
    public int Status { get; set; }
    public decimal Value { get; set; }
    public string BetUuiId { get; set; }

    public Transaction()
    {
        
    }

    private Transaction(Guid transactionId, int status, decimal value, string betId)
    {
        TransactionId = transactionId;
        Status = status;
        Value = value;
        BetUuiId = betId;
    }

    public static Transaction Create(Guid transactionId, int status, decimal value, string betUuiId)
    {
        var transaction = new Transaction(transactionId, status, value, betUuiId);

        return transaction;
    }

    public void UpdateStatus(int status)
    {
        Status = status;
    }
}
