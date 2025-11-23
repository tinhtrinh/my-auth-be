using Domain.Shared;
using MediatR;

namespace Application.Users.Login;

internal sealed class LoginCommandHandler 
    //: IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var accessToken = "Test login access token";
        var refreshToken = "Test login refresh token";
        var response = new LoginResponse(accessToken, refreshToken);
        return Task.FromResult(Result.Success(response));
    }
}
