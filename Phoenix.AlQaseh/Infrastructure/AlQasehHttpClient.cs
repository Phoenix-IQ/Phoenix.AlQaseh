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
    public async Task<AlQasehPaymentResponse> CreatePayment(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        using var response = await _client.PostAsJsonAsync(options.PaymentPath, request, cancellationToken);
        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        AlQasehPaymentResponse? result = JsonSerializer.Deserialize<AlQasehPaymentResponse>(content);
        return result ?? new AlQasehPaymentResponse
        {
            Err = "The payment request returned empty string",
            ErrorCode = "500"
        };
    }

}
