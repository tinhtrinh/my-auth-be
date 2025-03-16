using Domain.Shared;
using MediatR;

namespace Application.Users.Register;

public record RegisterCommand: IRequest<Result<RegisterResponse>>
{
    public string Name { get; }

    public string Password { get; }

    public RegisterCommand(RegisterRequest request)
    {
        Name = request.Name;
        Password = request.Password;
    }
}
