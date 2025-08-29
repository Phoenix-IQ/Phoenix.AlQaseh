using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Phoenix.AlQaseh.Models.Requests;

/// <summary>
/// Payment request model
/// </summary>
public sealed class CreatePaymentRequest
{
    /// <summary>Payment amount in the smallest unit expected by AlQaseh (e.g., IQD).</summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    /// <summary>Country (ISO 3166-1 alpha-2). Example: <c>IQ</c>.</summary>
    [JsonPropertyName("country"),DefaultValue("IQ")]
    [Required, StringLength(2, MinimumLength = 2)]
    public string Country { get; set; } = default!;

    /// <summary>Currency code (ISO 4217). Example: <c>IQD</c>.</summary>
    [JsonPropertyName("currency"),DefaultValue("IQD")]
    [Required, StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = default!;

    /// <summary>
    /// Arbitrary custom data you want to attach to the payment.
    /// </summary>
    [JsonPropertyName("custom_data")]
    public Dictionary<string, object?> CustomData { get; set; } = new();

    /// <summary>Human-readable description (shown to buyer/merchant).</summary>
    [JsonPropertyName("description")]
    [Required, StringLength(255)]
    public string Description { get; set; } = string.Empty;

    /// <summary>Buyer email (optional).</summary>
    [JsonPropertyName("email")]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>Merchant order identifier; must be between 1 and 32 characters (GUID without dashes).</summary>
    [JsonPropertyName("order_id")]
    [Required, StringLength(maximumLength:32, MinimumLength = 1,ErrorMessage = "OrderId must be between 1 and 32 characters")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>URL to redirect the buyer after completing the payment.</summary>
    [JsonPropertyName("redirect_url")]
    [Url]
    public string RedirectUrl { get; set; } = string.Empty;
    /// <summary>Transaction type as defined by AlQaseh (e.g., <c>Retail</c>).</summary>

    [JsonPropertyName("transaction_type"),DefaultValue("Retail")]
    [Required]
    public string TransactionType { get; set; } = default!;

    /// <summary>Webhook URL to receive asynchronous payment events (optional).</summary>
    [JsonPropertyName("webhook_url")]
    [Url]
    public string? WebhookUrl { get; set; }
}
