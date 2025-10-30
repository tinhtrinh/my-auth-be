using Application.Shared.Auth;
using Domain.Shared;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
}

//internal class JwtProvider : IJwtProvider
//{
//    private readonly JwtOptions _options;

//    public JwtProvider(IOptions<JwtOptions> options)
//    {
//        _options = options.Value;
//    }

//    public string GenerateAccessToken(/*User user*/)
//    {
//        var claims = new Claim[]
//        {
//            //new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
//            //new Claim(JwtRegisteredClaimNames.Name, user.Name)
//        };

//        var secretKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(_options.SecretKey));

//        var signingCredentials = new SigningCredentials(
//            secretKey, 
//            SecurityAlgorithms.HmacSha256);

//        var token = new JwtSecurityToken(
//            _options.Issuer,
//            _options.Audience,
//            claims,
//            null,
//            DateTime.UtcNow.AddMinutes(_options.ExpiryTimeInMinute),
//            signingCredentials);

//        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

//        return tokenValue;
//    }

//    public string GenerateRefreshToken()
//    {
//        var randomNumber = new byte[32];
//        using (var rng = RandomNumberGenerator.Create())
//        {
//            rng.GetBytes(randomNumber);
//            return Convert.ToBase64String(randomNumber);
//        }
//    }

//    public Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
//    {
//        var tokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//            ValidateIssuer = false,
//            ValidateIssuerSigningKey = false,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
//            ValidateLifetime = false,
//        };

//        var tokenHandler = new JwtSecurityTokenHandler();
//        SecurityToken securityToken;
//        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
//        var jwtSecurityToken = securityToken as JwtSecurityToken;
//        if (securityToken is null 
//            || jwtSecurityToken is null
//            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
//        {
//            return Result.Failure<ClaimsPrincipal>(new Error("AccessToken.Invalid", "Invalid Access Token"));
//        }

//        return Result.Success(principal);
//    }
//}
