namespace PoolbetIntegration.API.Features.Currencys;

public interface ICurrencyQuotes
{
    Task<decimal> ToEUR(string toCurrency, decimal credit, CancellationToken cancellationToken);
    Task<(decimal, CurrencyResponse)> ToBRL(string fromCurrency, decimal credit, CancellationToken cancellationToken);
    Task<(decimal, CurrencyResponse)> BrlToEur(decimal credit, CancellationToken cancellationToken);
    Task<(decimal, CurrencyResponse)> EurToBrl(decimal credit, CancellationToken cancellationToken);
    Task<(decimal, CurrencyResponse)> BgnToEur(decimal credit, CancellationToken cancellationToken);
    Task<(decimal, CurrencyResponse)> EurToBgn(decimal credit, CancellationToken cancellationToken);
    Task<(decimal, CurrencyResponse)> ConvertBRLToBGN(decimal credit, CancellationToken cancellationToken);
    Task<(decimal, CurrencyResponse)> ConvertBgnToBrl(decimal credit, CancellationToken cancellationToken);
}
