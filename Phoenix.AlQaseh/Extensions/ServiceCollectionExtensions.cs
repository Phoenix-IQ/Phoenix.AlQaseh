using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Phoenix.AlQaseh.Abstractions;
using Phoenix.AlQaseh.Configurations;
using Phoenix.AlQaseh.Infrastructure;
using System.Net.Http.Headers;
using System.Text;

namespace Phoenix.AlQaseh.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Bind from IConfiguration (appsettings / env) and register the SDK.
    /// </summary>
    public static IServiceCollection AddAlQaseh(this IServiceCollection services, IConfiguration configuration, string sectionPath)
    {
        services.AddOptions<AlQasehOptions>()
            .Bind(configuration.GetSection(sectionPath))
            .ValidateDataAnnotations()
            .Validate(o => Uri.IsWellFormedUriString(o.BaseUrl, UriKind.Absolute), "BaseUrl must be an absolute URL")
            .ValidateOnStart();

        RegisterHttpClientAndClient(services);
        return services;
    }

    /// <summary>
    /// Create Sandbox defaults and bind the settings to it.
    /// </summary>
    public static IServiceCollection AddAlQasehSandbox(this IServiceCollection services)
    {
        services.PostConfigure<AlQasehOptions>(o =>
        {
            var s = AlQasehOptions.CreateSandBox();
            if (string.IsNullOrWhiteSpace(o.BaseUrl)) o.BaseUrl = s.BaseUrl;
            if (string.IsNullOrWhiteSpace(o.ClientId)) o.ClientId = s.ClientId;
            if (string.IsNullOrWhiteSpace(o.ClientSecret)) o.ClientSecret = s.ClientSecret;
            if (string.IsNullOrWhiteSpace(o.PaymentPath)) o.PaymentPath = s.PaymentPath;
        });

        RegisterHttpClientAndClient(services);
        return services;
    }

    /// <summary>
    /// Create an instance of the options wwith passing the parameters to it.
    /// </summary>
    public static IServiceCollection AddAlQaseh(this IServiceCollection services, string baseUrl, string clientId, string clientSecret, string paymentPath = "egw/payments/create")
    {
        services.Configure<AlQasehOptions>(o =>
        {
            o.BaseUrl = baseUrl;
            o.ClientId = clientId;
            o.ClientSecret = clientSecret;
            o.PaymentPath = paymentPath;
        });

        services.AddOptions<AlQasehOptions>()
            .ValidateDataAnnotations()
            .Validate(o => Uri.IsWellFormedUriString(o.BaseUrl, UriKind.Absolute), "BaseUrl must be an absolute URL")
            .ValidateOnStart();

        RegisterHttpClientAndClient(services);
        return services;

    }
    private static void RegisterHttpClientAndClient(IServiceCollection services)
    {
        services.AddHttpClient(AlQasehHttpClient.ClientName)
            .ConfigureHttpClient((sp, http) =>
            {
                var opt = sp.GetRequiredService<IOptions<AlQasehOptions>>().Value;
                http.BaseAddress = new Uri(opt.BaseUrl);
                http.Timeout = TimeSpan.FromSeconds(30);

                var credentials = $"{opt.ClientId}:{opt.ClientSecret}";
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        services.AddScoped<IAlQasehHttpClient, AlQasehHttpClient>();
    }
}