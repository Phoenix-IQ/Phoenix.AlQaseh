using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.AlQaseh.Configurations;

public sealed class AlQasehOptions
{
    public AlQasehOptions() { }
    public static AlQasehOptions CreateProduction(string baseUrl, string clientId, string clientSecret,string paymentRequestPath,string paymentURL)
    {
        return new()
        {
            BaseUrl = baseUrl,
            ClientId = clientId,
            ClientSecret = clientSecret,
            PaymentRequestPath = paymentRequestPath,
            PaymentURL = paymentURL
        };
    }
    internal static AlQasehOptions CreateSandBox()
    {
        return new()
        {
            BaseUrl = "https://api-test.alqaseh.com/v1",
            ClientSecret = "Lr10yWWmm1dXLoI7VgXCrQVnlq13c1G0",
            ClientId = "public_test",
            PaymentRequestPath = "egw/payments/create",
            PaymentURL= "https://pay-test.alqaseh.com/pay/"
        };
    }

    [Required(ErrorMessage = "AlQaseh BaseURL is required and missing"), Url(ErrorMessage = "AlQaseh URL is invalid")]
    public string BaseUrl { get; set; } = default!;
    [Required(ErrorMessage = "AlQaseh ClientId is required and missing")]
    public string ClientId { get; set; } = default!;
    [Required(ErrorMessage = "AlQaseh ClientSecret is required and missing")]
    public string ClientSecret { get; set; } = default!;

    [Required(ErrorMessage ="AlQaseh Payment URL is required and missing")]
    public string PaymentRequestPath { get; set; } = default!;

    [Required(ErrorMessage ="AlQaseh PaymentURL is required"),Url(ErrorMessage ="AlQaseh paymentURL is invalid")]
    public string PaymentURL { get; set; } = string.Empty;

}