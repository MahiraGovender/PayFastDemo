using PayFastDemo.Client.Models;

namespace PayFastDemo.Client.Services;

public interface IPaymentApiService
{
    Task<string> CreatePaymentAsync(
        PaymentRequest request);
}