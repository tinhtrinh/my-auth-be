using Domain.Files;
using Domain.Users;

namespace Application.Users.DownloadAvatar;

public interface IDAUserService
{
    Task<FileRecord?> GetUserAvatarAsync(UserId userId);
}
