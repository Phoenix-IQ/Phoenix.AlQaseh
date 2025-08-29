using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Phoenix.AlQaseh.Models.Requests;

public sealed class CreatePaymentRequest
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("country"),DefaultValue("IQ")]
    [Required, StringLength(2, MinimumLength = 2)]
    public string Country { get; set; } = default!;

    [JsonPropertyName("currency"),DefaultValue("IQD")]
    [Required, StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = default!;

    /// <summary>
    /// Arbitrary custom data you want to attach to the payment.
    /// </summary>
    [JsonPropertyName("custom_data")]
    public Dictionary<string, object?> CustomData { get; set; } = new();

    [JsonPropertyName("description")]
    [Required, StringLength(255)]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    [EmailAddress]
    public string? Email { get; set; }

    [JsonPropertyName("order_id")]
    [Required, StringLength(maximumLength:32, MinimumLength = 1,ErrorMessage = "OrderId must be between 1 and 32 characters")]
    public string OrderId { get; set; } = string.Empty;

    [JsonPropertyName("redirect_url")]
    [Url]
    public string RedirectUrl { get; set; } = string.Empty;

    [JsonPropertyName("transaction_type"),DefaultValue("Retail")]
    [Required]
    public string TransactionType { get; set; } = default!;

    [JsonPropertyName("webhook_url")]
    [Url]
    public string? WebhookUrl { get; set; }
}
