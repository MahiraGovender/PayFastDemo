using Microsoft.AspNetCore.Mvc;
using PayFastDemo.Api.Models;
using PayFastDemo.Api.Services;

namespace PayFastDemo.Api.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPayFastService _payFastService;

    public PaymentController(
        IPayFastService payFastService)
    {
        _payFastService = payFastService;
    }

    [HttpPost]
    public IActionResult CreatePayment(
        PayFastPaymentRequest request)
    {
        var paymentForm =
            _payFastService.CreatePaymentForm(request);

        return Content(
            paymentForm,
            "text/html");
    }
}