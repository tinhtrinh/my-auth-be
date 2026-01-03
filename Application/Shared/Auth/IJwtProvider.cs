namespace Application.Shared.Auth;

public interface IJwtProvider
{
    Task<TokenResponse?> GetTokenAsync(string code);

    Task<TokenResponse?> RefreshTokenAsync(string refreshToken);

    Task<bool> LogoutAsync(string refreshToken);

    string? GetUserIdFromToken(string token);
}
