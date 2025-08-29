using Phoenix.AlQaseh.Models.Requests;
using Phoenix.AlQaseh.Models.Responses;

namespace Phoenix.AlQaseh.Abstractions;

/// <summary>
/// HTTP client abstraction for interacting with the AlQaseh payment gateway.
/// </summary>
public interface IAlQasehHttpClient
{
    /// <summary>
    /// Creates a new payment at AlQaseh.
    /// </summary>
    /// <param name="request">The payment request payload (amount, currency, order id, etc.).</param>
    /// <returns>
    /// A response that includes <c>payment_id</c> and <c>token</c> on success,
    /// or error fields (<c>err</c>, <c>error_code</c>, <c>reference_code</c>) on failure.
    /// </returns>
    Task<(bool ok, string? link, AlQasehPaymentResponse resp)> CreatePaymentLinkAsync(CreatePaymentRequest request, CancellationToken ct);
}
