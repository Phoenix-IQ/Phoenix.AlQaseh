using System.ComponentModel.DataAnnotations;

namespace Phoenix.AlQaseh.Configurations;

/// <summary>
/// Configuration options for the AlQaseh client.
/// </summary>
public sealed class AlQasehOptions
{
    /// <summary>
    /// Create an empty constructor for the options
    /// </summary>
    public AlQasehOptions() { }
    /// <summary>
    /// Create a production options
    /// </summary>
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
    /// <summary>
    /// API base URL. Example: <c>https://api-test.alqaseh.com/v1</c>.
    /// </summary>

    [Required(ErrorMessage = "AlQaseh BaseURL is required and missing"), Url(ErrorMessage = "AlQaseh URL is invalid")]
    public string BaseUrl { get; set; } = default!;

    /// <summary>
    /// Client identifier issued by AlQaseh.
    /// </summary>
    [Required(ErrorMessage = "AlQaseh ClientId is required and missing")]
    public string ClientId { get; set; } = default!;
    /// <summary>
    /// Client secret issued by AlQaseh.
    /// </summary>
    [Required(ErrorMessage = "AlQaseh ClientSecret is required and missing")]
    public string ClientSecret { get; set; } = default!;
    /// <summary>
    /// Relative path for the Create Payment endpoint (e.g., <c>egw/payments/create</c>).
    /// </summary>
    [Required(ErrorMessage ="AlQaseh Payment URL is required and missing")]
    public string PaymentRequestPath { get; set; } = default!;
    /// <summary>
    /// Relative URL for the payment url
    /// </summary>

    [Required(ErrorMessage ="AlQaseh PaymentURL is required"),Url(ErrorMessage ="AlQaseh paymentURL is invalid")]
    public string PaymentURL { get; set; } = string.Empty;

}