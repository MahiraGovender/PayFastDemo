using System.Net.Http.Json;
using PayFastDemo.Client.Models;

namespace PayFastDemo.Client.Services;

public class PaymentApiService : IPaymentApiService
{
    private readonly HttpClient _httpClient;

    public PaymentApiService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CreatePaymentAsync(
        PaymentRequest request)
    {
        var response =
            await _httpClient.PostAsJsonAsync(
                "api/payments",
                request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}