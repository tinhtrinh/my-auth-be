using Application.Abstractions;
using Domain.Shared;
using MediatR;

namespace Application.Users.Login;

internal sealed class LoginCommandHandler 
    //: IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var accessToken = _jwtProvider.GenerateAccessToken();
        var refreshToken = _jwtProvider.GenerateRefreshToken();
        var response = new LoginResponse(accessToken, refreshToken);
        return Task.FromResult(Result.Success(response));
    }
}
