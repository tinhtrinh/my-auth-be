using Domain.Shared;
using System.Security.Claims;

namespace Application.Shared.Auth;

public interface IJwtProvider
{
    Task<TokenResponse?> GetTokenAsync(string code);

    Task<TokenResponse?> RefreshTokenAsync(string refreshToken);

    //Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}
