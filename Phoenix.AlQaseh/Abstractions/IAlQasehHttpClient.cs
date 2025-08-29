using Phoenix.AlQaseh.Models.Requests;
using Phoenix.AlQaseh.Models.Responses;

namespace Phoenix.AlQaseh.Abstractions;

public interface IAlQasehHttpClient
{
    Task<AlQasehPaymentResponse> CreatePayment(CreatePaymentRequest request, CancellationToken cancellationToken);
}
