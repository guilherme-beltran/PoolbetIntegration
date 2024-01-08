using PoolbetIntegration.API.Features.Currencys;

namespace PoolbetIntegration.API.Features.UserAdmins;

public sealed class BalanceHandler : IBalanceHandler
{

    private readonly ICacheUserAdminRepository _repository;
    private readonly ICurrencyQuotes _quotes;
    private readonly Dictionary<string, Func<string, decimal, CancellationToken, Task<decimal>>> _currencyConverters;

    public BalanceHandler(ICacheUserAdminRepository repository, ICurrencyQuotes quotes)
    {
        _repository = repository;
        _quotes = quotes;

        _currencyConverters = new()
        {
            { "EUR", (_, credit, __) => Task.FromResult(credit) },
            { "BGN", (_, credit, __) => Task.FromResult(credit) },
            { "BRL", (_, credit, __) => Task.FromResult(credit) }
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

        decimal convertedCredit = new();

        if (_currencyConverters.TryGetValue(request.Currency, out var converter))
        {
            convertedCredit = await converter(request.Currency, userAdmin.Credit, cancellationToken);
            if (convertedCredit == 0)
            {
                return new BalanceResponse(status: false, 
                    credit: 0, 
                    error: "The service cannot convert the currency.", 
                    key: "convertedCredit");
            }
        }

        return new BalanceResponse(status: true,
                                   credit: Math.Round(convertedCredit, 2),
                                   currency: request.Currency,
                                   error: "");
    }
}
