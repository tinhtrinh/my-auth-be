using System.Text.Json.Serialization;

namespace Application.Shared.Auth;

public record TokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("expires_in")]
    public double ExpiresIn { get; set; }
}
