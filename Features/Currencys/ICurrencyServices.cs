namespace PoolbetIntegration.API.Features.Currencys;

public interface ICurrencyServices
{
    Task<CurrencyResponse> Convert(string fromCurrency, string toCurrency, CancellationToken cancellationToken);
}
