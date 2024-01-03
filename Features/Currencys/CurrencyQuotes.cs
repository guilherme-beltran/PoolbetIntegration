namespace PoolbetIntegration.API.Features.Currencys;

public class CurrencyQuotes : ICurrencyQuotes
{
    private readonly ICurrencyServices _services;

    public CurrencyQuotes(ICurrencyServices services)
    {
        _services = services;
    }

    public async Task<decimal> ToEUR(string toCurrency, decimal credit, CancellationToken cancellationToken)
    {
        var valueInEur = await _services.Convert("EUR", toCurrency, cancellationToken);
        var eurCredit = valueInEur.Value * credit;
        return eurCredit;
    }

    public async Task<decimal> ToBRL(string toCurrency, decimal credit, CancellationToken cancellationToken)
    {
        var valueInBrl = await _services.Convert("BRL", toCurrency, cancellationToken);

        var brlCredit = valueInBrl.Value * credit;
        return brlCredit;
    }

    public async Task<decimal> BrlToEur(decimal credit, CancellationToken cancellationToken)
    {
        var valueInBgn = await _services.Convert("BRL", "EUR", cancellationToken);
        if (valueInBgn.Status == 404)
        {
            return 0;
        }

        var bgnCredit = valueInBgn.Value * credit;
        return bgnCredit;
    }

    public async Task<decimal> EurToBgn(decimal credit, CancellationToken cancellationToken)
    {
        var valueInBgn = await _services.Convert("EUR", "BGN", cancellationToken);
        if (valueInBgn.Status == 404)
        {
            return 0;
        }

        var bgnCredit = valueInBgn.Value * credit;
        return bgnCredit;
    }

    public async Task<decimal> ConvertBRLToBGN(string toCurrency, decimal credit, CancellationToken cancellationToken)
    {
        var creditInEur = await BrlToEur(credit, cancellationToken);

        var creditInBgn = await EurToBgn(creditInEur, cancellationToken);
        return creditInBgn;
    }
}
