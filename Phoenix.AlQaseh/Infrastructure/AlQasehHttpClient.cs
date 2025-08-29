using Phoenix.AlQaseh.Abstractions;
using Phoenix.AlQaseh.Configurations;
using Phoenix.AlQaseh.Models.Requests;
using Phoenix.AlQaseh.Models.Responses;
using System.Text.Json;

namespace Phoenix.AlQaseh.Infrastructure;

public class AlQasehHttpClient(IHttpClientFactory clientFactory, AlQasehOptions options) : IAlQasehHttpClient
{
    public const string ClientName = "AlQasehClient";
    private readonly HttpClient _client = clientFactory.CreateClient(ClientName);
    private async Task<AlQasehPaymentResponse> CreatePayment(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        using var response = await _client.PostAsJsonAsync(options.PaymentRequestPath, request, cancellationToken);
        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        AlQasehPaymentResponse? result = JsonSerializer.Deserialize<AlQasehPaymentResponse>(content);
        return result ?? new AlQasehPaymentResponse
        {
            Err = "The payment request returned empty string",
            ErrorCode = "500"
        };
    }
    public async Task<(bool ok, string? link, AlQasehPaymentResponse resp)> CreatePaymentLinkAsync(CreatePaymentRequest request, CancellationToken ct)
    {
        var resp = await CreatePayment(request, ct);
        if (!resp.IsSuccess || string.IsNullOrWhiteSpace(resp.Token))
            return (false, null, resp);

        var link = $"{options.PaymentURL}{resp.Token}";
        return (true, link, resp);
    }

}
