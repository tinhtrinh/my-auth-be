using Domain.Users;

namespace Application.Users.GetUsers;

public class GetUsersDTO
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public string Password { get; private set; }

    private GetUsersDTO(string id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    public static GetUsersDTO Create(User user)
    {
        return new GetUsersDTO(user.Id.Value.ToString(), user.Name, user.Password);
    }
}