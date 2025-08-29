using Phoenix.AlQaseh.Models.Requests;
using Phoenix.AlQaseh.Models.Responses;

namespace Phoenix.AlQaseh.Abstractions;

public interface IAlQasehHttpClient
{
    Task<(bool ok, string? link, AlQasehPaymentResponse resp)> CreatePaymentLinkAsync(CreatePaymentRequest request, CancellationToken ct);
}
