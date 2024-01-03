namespace PoolbetIntegration.API.Features.Currencys;

public interface ICurrencyQuotes
{
    Task<decimal> ToEUR(string toCurrency, decimal credit, CancellationToken cancellationToken);
    Task<decimal> ToBRL(string toCurrency, decimal credit, CancellationToken cancellationToken);
    Task<decimal> BrlToEur(decimal credit, CancellationToken cancellationToken);
    Task<decimal> EurToBgn(decimal credit, CancellationToken cancellationToken);
    Task<decimal> ConvertBRLToBGN(string toCurrency, decimal credit, CancellationToken cancellationToken);
}
