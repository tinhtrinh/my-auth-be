using Domain.Users;

namespace Infrastructure.Authorization;

public interface IAuthorizationUserService
{
    Task<User?> GetUserWithRoles(UserId id);
}
