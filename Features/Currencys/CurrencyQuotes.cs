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

    public async Task<(decimal, CurrencyResponse)> ToBRL(string fromCurrency, decimal credit, CancellationToken cancellationToken)
        {
            var valueInBrl = await _services.Convert(fromCurrency: fromCurrency, toCurrency: "BRL", cancellationToken);
            if (valueInBrl.Status == 404)
            {
                var result = (0, new CurrencyResponse
                                     {
                                         Code = valueInBrl.Code,
                                         Status = valueInBrl.Status,
                                         Message = valueInBrl.Message
                                     });
                return result;
            }


            var brlCredit = valueInBrl.Value * credit;
            return (brlCredit, null);
        }

    public async Task<(decimal, CurrencyResponse)> BrlToEur(decimal credit, CancellationToken cancellationToken)
    {
        var valueInBgn = await _services.Convert(fromCurrency: "BRL", toCurrency: "EUR", cancellationToken);
        if (valueInBgn.Status == 404)
        {
            return (0, new CurrencyResponse
            {
                Code = valueInBgn.Code,
                Status = valueInBgn.Status,
                Message = valueInBgn.Message
            });
        }

        var bgnCredit = valueInBgn.Value * credit;
        return (bgnCredit, null);
    }

    public async Task<(decimal, CurrencyResponse)> EurToBgn(decimal credit, CancellationToken cancellationToken)
    {
        var valueInBgn = await _services.Convert(fromCurrency: "EUR", toCurrency: "BGN", cancellationToken);
        if (valueInBgn.Status == 404)
        {
            return (0, new CurrencyResponse
            {
                Code = valueInBgn.Code,
                Status = valueInBgn.Status,
                Message = valueInBgn.Message
            });
        }

        var bgnCredit = valueInBgn.Value * credit;
        return (bgnCredit, null);
    }

    public async Task<(decimal, CurrencyResponse)> ConvertBRLToBGN(decimal credit, CancellationToken cancellationToken)
    {
        var creditInEur = await BrlToEur(credit, cancellationToken);
        if (creditInEur.Item2 != null)
        {
            return creditInEur;
        }

        var creditInBgn = await EurToBgn(creditInEur.Item1, cancellationToken);
        return creditInBgn;
    }

    public async Task<(decimal, CurrencyResponse)> EurToBrl(decimal credit, CancellationToken cancellationToken)
    {
        var valueInBgn = await _services.Convert(fromCurrency: "EUR", toCurrency: "BRL", cancellationToken);
        if (valueInBgn.Status == 404)
        {
            return (0, new CurrencyResponse
            {
                Code = valueInBgn.Code,
                Status = valueInBgn.Status,
                Message = valueInBgn.Message
            });
        }

        var bgnCredit = valueInBgn.Value * credit;
        return (bgnCredit, null);
    }

    public async Task<(decimal, CurrencyResponse)> ConvertBgnToBrl(decimal credit, CancellationToken cancellationToken)
    {
        var creditInEur = await BgnToEur(credit, cancellationToken);
        if (creditInEur.Item2 != null)
        {
            return creditInEur;
        }

        var creditInBrl = await EurToBrl(creditInEur.Item1, cancellationToken);
        return creditInBrl;
    }

    public async Task<(decimal, CurrencyResponse)> BgnToEur(decimal credit, CancellationToken cancellationToken)
    {
        var valueInBgn = await _services.Convert(fromCurrency: "EUR", toCurrency: "BGN", cancellationToken);
        if (valueInBgn.Status == 404)
        {
            return (0, new CurrencyResponse
            {
                Code = valueInBgn.Code,
                Status = valueInBgn.Status,
                Message = valueInBgn.Message
            });
        }

        var eurCredit = credit / valueInBgn.Value;
        return (eurCredit, null);
    }
}
