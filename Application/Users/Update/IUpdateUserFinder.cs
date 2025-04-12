using Domain.Users;

namespace Application.Users.Update;

public interface IUpdateUserFinder
{
    Task<User?> FindAsync(string id);
}
