using Domain.Shared;
using MediatR;

namespace Application.Users.Update;

public class UpdateCommand : IRequest<Result>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public UpdateCommand(UpdateRequest request)
    {
        Id = request.Id;
        Name = request.Name;
        Password = request.Password;
    }
}
