using Newtonsoft.Json.Linq;
using PoolbetIntegration.API.Features.Currencys;

namespace PoolbetIntegration.API.Features.UserAdmins;

public sealed class BalanceHandler : IBalanceHandler
{

    private readonly ICacheUserAdminRepository _repository;
    private readonly ICurrencyQuotes _quotes;
    private readonly Dictionary<string, Func<decimal, CancellationToken, Task<(decimal, CurrencyResponse)>>> _currencyConverters;

    public BalanceHandler(ICacheUserAdminRepository repository, ICurrencyQuotes quotes)
    {
        _repository = repository;
        _quotes = quotes;

        _currencyConverters = new()
        {
            { "EUR", _quotes.BrlToEur },
            { "BGN", _quotes.ConvertBRLToBGN }
        };
    }

    public async Task<BalanceResponse> Handle(BalanceRequest request, CancellationToken cancellationToken)
    {
        var userAdmin = await _repository.GetBalanceAsync(username: request.Username, 
            email: request.Email, 
            cancellationToken: cancellationToken);
        if (userAdmin is null)
        {
            return new BalanceResponse(status: false, 
                credit: 0.00m, 
                error: $"No users was found with username {request.Username} and email {request.Email}.", 
                key: "LoginController.GetBalance");
        }

        (decimal, CurrencyResponse) convertedCredit = new();
        (decimal, CurrencyResponse) anotherResult = new();

        if (_currencyConverters.TryGetValue(request.Currency.ToUpper(), out var converter))
        {
            convertedCredit = await converter(userAdmin.Credit, cancellationToken);
            if (convertedCredit.Item1 == 0)
            {
                return new BalanceResponse(status: false,
                                           credit: 0,
                                           error: "The service cannot convert the currency.",
                                           key: "convertedCredit");
            }

            return new BalanceResponse(status: true,
                                       credit: Math.Round(convertedCredit.Item1, 2),
                                       currency: request.Currency.ToUpper(),
                                       error: "");
        }

        return new BalanceResponse(status: true,
                                       credit: Math.Round(userAdmin.Credit, 2),
                                       currency: request.Currency.ToUpper(),
                                       error: "");

    }
}
