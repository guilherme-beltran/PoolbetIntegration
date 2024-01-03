using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace PoolbetIntegration.API.Features.Currencys;

public class CurrencyServices : ICurrencyServices
{
    private readonly IHttpClientFactory _client;

    public CurrencyServices(IHttpClientFactory client)
    {
        _client = client;
    }

    public async Task<CurrencyResponse> Convert(string fromCurrency, string toCurrency, CancellationToken cancellationToken)
    {
        var client = _client.CreateClient("economia");

        try
        {
            var response = await client.GetAsync($"/json/{fromCurrency}-{toCurrency}", cancellationToken);
            var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var currencyError = JsonConvert.DeserializeObject<CurrencyError>(responseMessage);
                return new CurrencyResponse 
                { 
                    Code = currencyError.Code,
                    Status = currencyError.Status,
                    Message = currencyError.Message
                };
            }

            
            var currency = JsonConvert.DeserializeObject<List<Currency>>(responseMessage);
            var lowValue = decimal.Parse(currency[0].Low, CultureInfo.InvariantCulture);
            var highValue = decimal.Parse(currency[0].High, CultureInfo.InvariantCulture);
            var media = (lowValue + highValue) / 2;
            return new CurrencyResponse { Value = media };
        }
        catch (HttpRequestException ex)
        {
            await Console.Out.WriteLineAsync($"HTTP Request Exception: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            await Console.Out.WriteLineAsync($"JSON Deserialization Exception: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Unexpected Exception: {ex.Message}");
            throw;
        }
    }
}
