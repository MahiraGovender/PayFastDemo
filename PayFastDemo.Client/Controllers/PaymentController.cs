using Microsoft.AspNetCore.Mvc;
using PayFastDemo.Client.Models;
using PayFastDemo.Client.Services;

namespace PayFastDemo.Client.Controllers;

public class PaymentController : Controller
{
    private readonly IPaymentApiService _paymentApiService;

    public PaymentController(
        IPaymentApiService paymentApiService)
    {
        _paymentApiService = paymentApiService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        PaymentRequest request)
    {
        var paymentForm =
            await _paymentApiService
                .CreatePaymentAsync(request);

        return Content(
            paymentForm,
            "text/html");
    }

    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Cancel()
    {
        return View();
    }
}