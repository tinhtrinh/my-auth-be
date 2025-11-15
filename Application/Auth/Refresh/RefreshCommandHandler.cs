using Application.Shared.Auth;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Auth.Refresh;

public sealed class RefreshCommandHandler : IRequestHandler<RefreshCommand, Result<RefreshResponse>>
{
    private readonly IJwtProvider _jwtProvider;

    public RefreshCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<RefreshResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = request.RefreshToken;

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Result.Failure<RefreshResponse>(UserError.RefreshTokenNotFound);
        }

        var tokenResponse = await _jwtProvider.RefreshTokenAsync(refreshToken);

        if(tokenResponse is null)
        {
            return Result.Failure<RefreshResponse>(UserError.GetJwtFail);
        }

        var newAccessToken = tokenResponse.AccessToken;
        var newRefreshToken = tokenResponse.RefreshToken;
        var newExpiresIn = tokenResponse.ExpiresIn;

        if(string.IsNullOrEmpty(newAccessToken) || string.IsNullOrEmpty(newRefreshToken) || newExpiresIn == 0)
        {
            return Result.Failure<RefreshResponse>(UserError.GetJwtFail);
        }

        var newExpiresInUtc = DateTime.UtcNow.AddSeconds(newExpiresIn).ToString();
        var response = new RefreshResponse(newAccessToken, newRefreshToken, newExpiresInUtc);
        return Result.Success(response);
    }
}
