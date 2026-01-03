using Application.Shared.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;

namespace Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<JwtProvider> _logger;

    public JwtProvider(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory, 
        ILogger<JwtProvider> logger)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<TokenResponse?> GetTokenAsync(string code)
    {
        var redirectUri = _configuration["IdentityProviders:Keycloak:RedirectUri"];
        var clientId = _configuration["IdentityProviders:Keycloak:ClientId"];
        var clientSecret = _configuration["IdentityProviders:Keycloak:ClientSecret"];
        var tokenEndpoint = _configuration["IdentityProviders:Keycloak:TokenEndpoint"];

        if (string.IsNullOrEmpty(redirectUri))
        {
            throw new ArgumentNullException("RedirectUri can not be null!");
        }

        if (string.IsNullOrEmpty(clientId))
        {
            throw new ArgumentNullException("ClientId can not be null!");
        }

        if (string.IsNullOrEmpty(clientSecret))
        {
            throw new ArgumentNullException("ClientSecret can not be null!");
        }

        if (string.IsNullOrEmpty(tokenEndpoint))
        {
            throw new ArgumentNullException("TokenEndpoint can not be null!");
        }

        var parameters = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "code", code },
            { "redirect_uri", redirectUri },
            { "client_id", clientId },
            { "client_secret", clientSecret }
        };
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(parameters));
        if (!response.IsSuccessStatusCode)
        {
            var statusCode = response.StatusCode;
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError(
                "Token request failed. StatusCode: {StatusCode}, Response: {ErrorContent}", 
                statusCode, 
                errorContent);
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<TokenResponse>();
        var accessToken = content?.AccessToken;
        var expiresIn = content?.ExpiresIn ?? 0;
        var refreshToken = content?.RefreshToken;
        if (string.IsNullOrEmpty(accessToken)
            || string.IsNullOrEmpty(refreshToken)
            || expiresIn == 0)
        {
            return null;
        }

        return content;
    }

    public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        var clientId = _configuration["IdentityProviders:Keycloak:ClientId"];
        var clientSecret = _configuration["IdentityProviders:Keycloak:ClientSecret"];
        var tokenEndpoint = _configuration["IdentityProviders:Keycloak:TokenEndpoint"];

        if (string.IsNullOrEmpty(clientId))
        {
            throw new ArgumentNullException("ClientId can not be null!");
        }

        if (string.IsNullOrEmpty(clientSecret))
        {
            throw new ArgumentNullException("ClientId can not be null!");
        }

        if (string.IsNullOrEmpty(tokenEndpoint))
        {
            throw new ArgumentNullException("TokenEndpoint can not be null!");
        }

        var client = _httpClientFactory.CreateClient();
        var parameters = new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken },
            { "client_secret", clientSecret }
        };
        var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(parameters));
        if (!response.IsSuccessStatusCode)
        {
            var statusCode = response.StatusCode;
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError(
                "Token request failed. StatusCode: {StatusCode}, Response: {ErrorContent}",
                statusCode,
                errorContent);
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<TokenResponse>();
        var newAccessToken = content?.AccessToken;
        var NewExpiresIn = content?.ExpiresIn ?? 0;
        var newRefreshToken = content?.RefreshToken;
        if (string.IsNullOrEmpty(newAccessToken)
            || string.IsNullOrEmpty(newRefreshToken)
            || NewExpiresIn == 0)
        {
            return null;
        }

        return content;
    }

    public async Task<bool> LogoutAsync(string refreshToken)
    {
        var clientId = _configuration["IdentityProviders:Keycloak:ClientId"];
        var clientSecret = _configuration["IdentityProviders:Keycloak:ClientSecret"];
        var logoutEndpoint = _configuration["IdentityProviders:Keycloak:LogoutEndpoint"];

        if (string.IsNullOrEmpty(clientId))
        {
            throw new ArgumentNullException("ClientId can not be null!");
        }

        if (string.IsNullOrEmpty(clientSecret))
        {
            throw new ArgumentNullException("ClientId can not be null!");
        }

        if (string.IsNullOrEmpty(logoutEndpoint))
        {
            throw new ArgumentNullException("LogoutEndpoint can not be null!");
        }

        var client = _httpClientFactory.CreateClient();
        var parameters = new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "refresh_token", refreshToken },
            { "client_secret", clientSecret }
        };
        var response = await client.PostAsync(logoutEndpoint, new FormUrlEncodedContent(parameters));
        if (!response.IsSuccessStatusCode)
        {
            var statusCode = response.StatusCode;
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError(
                "Logout request failed. StatusCode: {StatusCode}, Response: {ErrorContent}",
                statusCode,
                errorContent);
            return false;
        }

        return true;
    }

    public string? GetUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
    }
}
