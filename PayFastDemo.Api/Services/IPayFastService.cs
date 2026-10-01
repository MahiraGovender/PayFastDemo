using PayFastDemo.Api.Models;

namespace PayFastDemo.Api.Services;

public interface IPayFastService
{
    string CreatePaymentForm(PayFastPaymentRequest request);
}