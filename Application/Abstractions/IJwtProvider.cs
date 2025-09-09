using Domain.Shared;
using Domain.Users;
using System.Security.Claims;

namespace Application.Abstractions;

public interface IJwtProvider
{
    string GenerateAccessToken(/*User user*/);

    string GenerateRefreshToken();

    Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}