namespace PoolbetIntegration.API.Features.Transactions;

public interface ISetTransactionHandler
{
    Task<TransactionResponse> Handle(TransactionRequest request, CancellationToken cancellationToken);
}