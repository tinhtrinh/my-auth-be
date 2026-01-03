using Application.Shared.Auth;
using Domain.Shared;
using MediatR;

namespace Application.Auth.LoginCallback;

public sealed class LoginCallbackCommandHandler : IRequestHandler<LoginCallbackCommand, Result<LoginCallbackResponse>>
{
    private readonly IJwtProvider _jwtProvider;

    public LoginCallbackCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginCallbackResponse>> Handle(LoginCallbackCommand request, CancellationToken cancellationToken)
    {
        var code = request.Code;
        var tokenResponse = await _jwtProvider.GetTokenAsync(code);

        if(tokenResponse is null)
        {
            return Result.Failure<LoginCallbackResponse>(new Error("Jwt.Fail", "Get JWT fail"));
        }

        var accessToken = tokenResponse.AccessToken;
        var refreshToken = tokenResponse.RefreshToken;
        var expiresIn = tokenResponse.ExpiresIn;

        if(string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken) || expiresIn == 0)
        {
            return Result.Failure<LoginCallbackResponse>(new Error("Jwt.Fail", "Get JWT fail"));
        }

        var expiresInUtc = DateTime.UtcNow.AddSeconds(expiresIn).ToString("O");
        var response = new LoginCallbackResponse(accessToken, refreshToken, expiresInUtc);
        return Result.Success(response);
    }
}
