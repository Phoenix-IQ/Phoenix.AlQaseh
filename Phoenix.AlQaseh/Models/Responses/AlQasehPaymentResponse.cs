using System.Text.Json.Serialization;

namespace Phoenix.AlQaseh.Models.Responses;
/// <summary>
/// Response returned by Create Payment. Inherits error fields for convenience.
/// </summary>
public sealed class AlQasehPaymentResponse:AlQasehErrorResponse
{
    /// <summary>Identifier of the created payment.</summary>
    [JsonPropertyName("payment_id")] 
    public string PaymentId { get; set; } = string.Empty;
    /// <summary>Token used by the checkout page to complete the payment.</summary>
    [JsonPropertyName("token")] 
    public string Token { get; set; } = string.Empty;
    /// <summary>Indicates whether the response is successful (no <c>ErrorCode</c> present).</summary>
    public bool IsSuccess => string.IsNullOrEmpty(ErrorCode);

}
