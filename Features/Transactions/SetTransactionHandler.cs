using PoolbetIntegration.API.Features.Currencys;
using PoolbetIntegration.API.Features.UnitOfWork;
using PoolbetIntegration.API.Features.UserAdmins;

namespace PoolbetIntegration.API.Features.Transactions;

public sealed class SetTransactionHandler : ISetTransactionHandler
{
    private readonly ICacheUserAdminRepository _cacheUserRepository;
    private readonly ICacheTransactionRepository _cacheTransactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetTransactionHandler> _logger;
    private readonly ICurrencyQuotes _quotes;
    private readonly Dictionary<string, Func<decimal, CancellationToken, Task<(decimal, CurrencyResponse)>>> _currencyConverters;

    public SetTransactionHandler(ICacheUserAdminRepository cacheUserRepository,
                                    ICacheTransactionRepository cacheTransactionRepository,
                                    ILogger<SetTransactionHandler> logger,
                                    IUnitOfWork unitOfWork,
                                    ICurrencyQuotes quotes)
    {
        _cacheUserRepository = cacheUserRepository;
        _cacheTransactionRepository = cacheTransactionRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _quotes = quotes;

        _currencyConverters = new()
        {
            { "EUR", _quotes.EurToBrl },
            { "BGN", _quotes.ConvertBgnToBrl }
        };
    }

    public async Task<TransactionResponse> Handle(TransactionRequest request, CancellationToken cancellationToken)
    {
        var user = await _cacheUserRepository.GetByUsernameAndEmailAsync(username: request.Username!,
                                                                email: request.Email!,
                                                                cancellationToken);


        user!.Clear();
        _logger.LogInformation($"Old credit: {user!.Credit}");

        if (request.Value < 0)
        {
            request.Value = Math.Abs(request.Value);
        }

        if (request.Type == 3)
        {
            try
            {
                var transaction = await _cacheTransactionRepository.GetByBetIdAsync(request.BetId!);
                transaction.UpdateStatus(status: 3);
                var updated = await _cacheTransactionRepository.UpdateAsync(transaction);
                if (!updated)
                {
                    _logger.LogInformation($"Failed transaction.");
                    await _unitOfWork.Rollback();
                    return new TransactionResponse(status: false, user.Credit, "The transaction could not be saved");
                }

                await _unitOfWork.Commit(cancellationToken);

                return new TransactionResponse(status: true, user.Credit, $"Successful transaction.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        (decimal, CurrencyResponse) convertedCredit = new();
        if (_currencyConverters.TryGetValue(request.Currency.ToUpper(), out var converter))
        {
            convertedCredit = await converter(request.Value, cancellationToken);
            if (convertedCredit.Item2 is not null)
            {
                return new TransactionResponse(status: false, credit: 0.0m, convertedCredit.Item2.Message!);
            }

            request.Value = Math.Round(convertedCredit.Item1, 2);
        }

        user!.VerifyValue(request.Value, request.Type);
        if (!user.IsValid)
        {
            _logger.LogInformation($"Failed transaction.");
            await _unitOfWork.Rollback();
            return new TransactionResponse(status: false, user.Credit, "Amount is greater than current balance.");
        }

        _logger.LogInformation($"New credit: {user!.Credit}");

        try
        {
            _unitOfWork.BeginTransaction();
            var updatedCreditUser = await _cacheUserRepository.UpdateBalance(user, cancellationToken);

            if (!updatedCreditUser)
            {
                _logger.LogInformation($"Failed transaction.");
                await _unitOfWork.Rollback();
                return new TransactionResponse(status: false, user.Credit, "Balance too low.");
            }

            var transaction = Transaction.Create(transactionId: Guid.NewGuid(), status: 1, value: request.Value, betUuiId: request.BetId!);
            await _cacheTransactionRepository.Insert(transaction, cancellationToken);

            _logger.LogInformation($"Successful transaction.");
            await _unitOfWork.Commit(cancellationToken);
            return new TransactionResponse(status: true, user.Credit, $"Successful transaction.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception($"{ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
