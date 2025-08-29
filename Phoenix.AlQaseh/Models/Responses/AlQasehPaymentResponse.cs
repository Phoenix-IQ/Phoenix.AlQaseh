using System.Text.Json.Serialization;

namespace Phoenix.AlQaseh.Models.Responses;
public sealed class AlQasehPaymentResponse:AlQasehErrorResponse
{
    [JsonPropertyName("payment_id")] 
    public string PaymentId { get; set; } = string.Empty;
    [JsonPropertyName("token")] 
    public string Token { get; set; } = string.Empty;

    public bool IsSuccess => string.IsNullOrEmpty(ErrorCode);

}
