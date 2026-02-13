using Application.Users.DownloadAvatar;
using Domain.Files;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Users.DownloadAvatar;

public class DAUserService : IDAUserService
{
    private readonly MyAuthDbContext _dbContext;

    public DAUserService(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileRecord?> GetUserAvatarAsync(UserId userId)
    {
        var avatar = await _dbContext.Set<User>()
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => u.AvatarId == null || u.Avatar == null ?
                null
                : new FileRecord.Builder(u.AvatarId ?? Guid.Empty, u.Avatar.FileName)
                    .SetPath(u.Avatar.Path)
                    .SetContentType(u.Avatar.ContentType)
                    .Build()
            ).FirstOrDefaultAsync();

        return avatar;
    }
}
