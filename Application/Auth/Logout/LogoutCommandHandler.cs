using Application.Shared.Auth;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Auth.Logout;

internal sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IJwtProvider _jwtProvider;

    public LogoutCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = request.RefreshToken;

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Result.Failure(UserError.RefreshTokenNotFound);
        }

        var result = await _jwtProvider.LogoutAsync(refreshToken);

        if (!result) return Result.Failure(UserError.LogoutFail);

        return Result.Success();
    }
}
