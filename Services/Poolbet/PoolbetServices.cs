using Newtonsoft.Json;
using PoolbetIntegration.API.Features;
using PoolbetIntegration.API.Features.Login;

namespace PoolbetIntegration.API.Services.Poolbet;

public sealed class PoolbetServices : IPoolbetServices
{
    private readonly IHttpClientFactory _client;

    public PoolbetServices(IHttpClientFactory client)
    {
        _client = client;
    }

    public async Task<Response> SendLogin(LoginRequest request)
    {
        var client = _client.CreateClient("poolbet");

        try
        {
            var response = await client.PostAsJsonAsync("/users/login", request);
            var responseData = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync($"Request to /users/login returned status code: {response.StatusCode}\n");

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Error response: {responseData}");
                return new Response { Message = "Error occurred" };
            }

            var responseObject = JsonConvert.DeserializeObject<Response>(responseData);
            await Console.Out.WriteLineAsync($"User: {responseObject?.User}\nToken: {responseObject?.Token}\nMessage: {responseObject?.Message}");

            return responseObject;
        }
        catch (HttpRequestException ex)
        {
            await Console.Out.WriteLineAsync($"HTTP Request Exception: {ex.Message}");
            return new Response { Message = "An error occurred while communicating with the authentication service." };
        }
        catch (JsonException ex)
        {
            await Console.Out.WriteLineAsync($"JSON Deserialization Exception: {ex.Message}");
            return new Response { Message = "An error occurred while processing the response from the authentication service." };
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Unexpected Exception: {ex.Message}");
            return new Response { Message = "An unexpected error occurred while processing the request." };
        }
    }


}
