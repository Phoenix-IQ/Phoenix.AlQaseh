# Phoenix.AlQaseh

A lightweight, DI-friendly .NET 9 SDK for the **AlQaseh** payment gateway.  
This single README shows **configuration** and **how to use** the client to create a payment.

---

## Install

```bash
dotnet add package Phoenix.AlQaseh
```

*(If you’re consuming from GitHub Packages instead of NuGet.org, add the GPR source first.)*

---

## Configuration

### appsettings.json

```json
{
  "Phoenix": {
    "AlQaseh": {
      "BaseUrl": "https://api-test.alqaseh.com/v1",
      "ClientId": "public_test",
      "ClientSecret": "set-in-user-secrets",
      "PaymentPath": "egw/payments/create"
    }
  }
}
```

> **Fields**
> - `BaseUrl` — AlQaseh API base URL (sandbox or production)
> - `ClientId` / `ClientSecret` — credentials issued by AlQaseh
> - `PaymentPath` — relative path for *Create Payment*, e.g., `egw/payments/create`

### Register the SDK (Dependency Injection)

**Option A — Bind from configuration**
```csharp
using Phoenix.AlQaseh.Extensions;

builder.Services.AddAlQaseh(builder.Configuration, "Phoenix:AlQaseh");
```

**Option B — Provide values explicitly (no IConfiguration)**
```csharp
using Phoenix.AlQaseh.Extensions;

builder.Services.AddAlQaseh(
    baseUrl: "https://api.alqaseh.com/v1",
    clientId: "<client-id>",
    clientSecret: "<client-secret>",
    paymentPath: "egw/payments/create");
```

**Option C — Sandbox with sensible defaults (fills missing values)**
```csharp
using Phoenix.AlQaseh.Extensions;

builder.Services.AddAlQasehSandbox();
```

> Under the hood, a named `HttpClient` **"AlQasehClient"** is registered with:
> - `BaseAddress = BaseUrl`
> - `Authorization: Basic base64(clientId:clientSecret)`
> - `Accept: application/json`

---

## How to Use

**Interface**
```csharp
namespace Phoenix.AlQaseh.Abstractions
{
    public interface IAlQasehHttpClient
    {
        Task<AlQasehPaymentResponse> CreatePayment(CreatePaymentRequest request, CancellationToken cancellationToken);
    }
}
```

**Create a payment**
```csharp
using Phoenix.AlQaseh.Abstractions;
using Phoenix.AlQaseh.Models.Requests;

var client = app.Services.GetRequiredService<IAlQasehHttpClient>();

var request = new CreatePaymentRequest
{
    Amount = 1m,
    Country = "IQ",                   // ISO 3166-1 alpha-2
    Currency = "IQD",                 // ISO 4217
    Description = "Testing",
    Email = "",
    OrderId = Guid.NewGuid().ToString("N"),  // exactly 32 chars (GUID without dashes)
    RedirectUrl = "https://google.com",
    TransactionType = "Retail",
    WebhookUrl = "",
    CustomData = new()
    {
        ["invoiceId"] = "INV-12345",
        ["meta"] = new { any = "thing" }
    }
};

var resp = await client.CreatePayment(request, CancellationToken.None);

if (resp.IsSuccess)
{
    Console.WriteLine($"payment_id={resp.PaymentId}, token={resp.Token}");
    // If your checkout page is token-based, construct the pay URL per AlQaseh docs, e.g.:
    // var payUrl = $"https://pay.alqaseh.com/{resp.Token}";
    // Console.WriteLine($"Pay URL: {payUrl}");
}
else
{
    Console.WriteLine($"Error: code={resp.ErrorCode}, msg={resp.Err}, ref={resp.ReferenceCode}");
}
```

**Success (HTTP 200) response shape**
```json
{ "payment_id": "string", "token": "string" }
```

**Error (HTTP 400/401) response shape**
```json
{ "err": "string", "error_code": "string", "reference_code": "string" }
```

---

## License

MIT
