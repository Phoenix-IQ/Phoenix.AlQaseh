using System.Text.Json.Serialization;

namespace Phoenix.AlQaseh.Models.Responses;

/// <summary>
/// Error envelope returned by AlQaseh on non-successful responses.
/// </summary>
public class AlQasehErrorResponse
{
    /// <summary>Returned Error.</summary>
    [JsonPropertyName("err")]
    public string? Err { get; set; } = string.Empty;
    /// <summary>Returned Error code.</summary>
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; } = string.Empty;
    /// <summary>Returned Refrence code.</summary>
    [JsonPropertyName("reference_code")]
    public string? ReferenceCode { get; set; } = string.Empty;
}
