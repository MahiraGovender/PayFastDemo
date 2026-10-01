namespace PayFastDemo.Api.Models;

public class PayFastPaymentRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string PaymentId { get; set; } = string.Empty;

    public string ItemName { get; set; } = string.Empty;

    public string? ItemDescription { get; set; }
}