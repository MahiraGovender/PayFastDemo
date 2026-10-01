using System.Security.Cryptography;
using System.Text;
using PayFastDemo.Api.Models;

namespace PayFastDemo.Api.Services;

public class PayFastService : IPayFastService
{
    private readonly IConfiguration _configuration;

    public PayFastService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreatePaymentForm(PayFastPaymentRequest request)
    {
        var merchantId =
            _configuration["PayFast:MerchantId"];

        var merchantKey =
            _configuration["PayFast:MerchantKey"];

        var passphrase =
            _configuration["PayFast:Passphrase"];

        var baseUrl =
            _configuration["PayFast:BaseUrl"];

        var data =
            new List<KeyValuePair<string, string>>
            {
                new("merchant_id", merchantId!),
                new("merchant_key", merchantKey!),

                new(
                    "return_url",
                    "http://localhost:5078/Payment/Success"),

                new(
                    "cancel_url",
                    "http://localhost:5078/Payment/Cancel"),

                new(
                    "name_first",
                    request.FirstName),

                new(
                    "name_last",
                    request.LastName),

                new(
                    "email_address",
                    request.Email),

                new(
                    "m_payment_id",
                    request.PaymentId),

                new(
                    "amount",
                    request.Amount.ToString("0.00")),

                new(
                    "item_name",
                    request.ItemName)
            };

        if (!string.IsNullOrWhiteSpace(
            request.ItemDescription))
        {
            data.Add(
                new(
                    "item_description",
                    request.ItemDescription));
        }

        var signature =
            GenerateSignature(
                data,
                passphrase!);

        data.Add(
            new(
                "signature",
                signature));

        return BuildHtmlForm(
            $"{baseUrl}/eng/process",
            data);
    }

    private string GenerateSignature(List<KeyValuePair<string, string>> data, string passphrase)
    {
        var parameters =
            new List<string>();

        foreach (var item in data)
        {
            if (!string.IsNullOrWhiteSpace(item.Value))
            {
                var value =
                    item.Value.Trim();

                value =
                    Uri.EscapeDataString(value)
                        .Replace("%20", "+");

                parameters.Add(
                    $"{item.Key}={value}");
            }
        }

        var parameterString =
            string.Join("&", parameters);

        parameterString +=
            $"&passphrase={Uri.EscapeDataString(passphrase.Trim()).Replace("%20", "+")}";

        using var md5 = MD5.Create();

        var hash =
            md5.ComputeHash(
                Encoding.UTF8.GetBytes(parameterString));

        return Convert
            .ToHexString(hash)
            .ToLowerInvariant();
    }

    private string BuildHtmlForm(
        string action,
        List<KeyValuePair<string, string>> data)
    {
        var html =
            new StringBuilder();

        html.Append(
            $"<form action=\"{action}\" method=\"post\">");

        foreach (var item in data)
        {
            html.Append(
                $"<input type=\"hidden\" " +
                $"name=\"{item.Key}\" " +
                $"value=\"{item.Value}\" />");
        }

        html.Append(
            "<button type=\"submit\">" +
            "Pay with PayFast" +
            "</button>");

        html.Append("</form>");

        return html.ToString();
    }
}