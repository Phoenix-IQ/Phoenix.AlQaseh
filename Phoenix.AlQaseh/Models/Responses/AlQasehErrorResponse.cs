using System.Text.Json.Serialization;

namespace Phoenix.AlQaseh.Models.Responses;

public  class AlQasehErrorResponse
{
    [JsonPropertyName("err")]
    public string? Err { get; set; } = string.Empty;
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; } = string.Empty;
    [JsonPropertyName("reference_code")]
    public string? ReferenceCode { get; set; } = string.Empty;
}
