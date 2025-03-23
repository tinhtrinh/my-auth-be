using Domain.Users;
namespace Application.Users;

public interface IUserRepository
{
    Task<bool> IsNameUnique(string name);

    void Add(User user);
}
