using Domain.Users;
namespace Application.Users;

public interface IUserRepository
{
    Task<bool> IsNameUnique(string name);

    Task<bool> IsExisted(string id);

    void Add(User user);

    void Update(User user);
}
