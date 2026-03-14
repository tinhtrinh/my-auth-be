using Application.Users.UploadAvatar;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Users.UploadAvatar;

public class UAUserRepository : IUAUserRepository
{
    private readonly MyAuthDbContext _myAuthDbContext;

    public UAUserRepository(MyAuthDbContext myAuthDbContext)
    {
        _myAuthDbContext = myAuthDbContext;
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        Guid uGuid = Guid.Empty;
        if(!Guid.TryParse(userId, out uGuid))
        {
            return null;
        }

        var uid = new UserId(uGuid);
        var user = await _myAuthDbContext.Set<User>()
            .AsNoTracking()
            .Where(u => u.Id == uid)
            .Select(u => new User.Builder(u.Id, u.Name)
                .SetAvatar(u.Avatar)
                .Build()
            )
            .FirstOrDefaultAsync();

        return user;
    }

    public void Update(User user)
    {

        if (user.Avatar is null)
        {
            throw new ArgumentNullException("User Avatar is null");
        }

        _myAuthDbContext.Attach(user);
        _myAuthDbContext.Entry(user.Avatar).Property(a => a.FileName).IsModified = true;
        _myAuthDbContext.Entry(user.Avatar).Property(a => a.Path).IsModified = true;
    }
}
