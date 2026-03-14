using Domain.Users;

namespace Application.Users.UploadAvatar;

public interface IUAUserRepository
{
    Task<User?> GetUserByIdAsync(string userId);

    void Update(User user);
}
