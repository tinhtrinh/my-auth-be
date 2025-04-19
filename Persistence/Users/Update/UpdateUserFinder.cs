using Application.Users.Update;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Users.Update;

public class UpdateUserFinder : IUpdateUserFinder
{
    private readonly MyAuthDbContext _dbContext;

    public UpdateUserFinder(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> FindAsync(string id)
    {
        var userId = new UserId(new Guid(id));

        var user = await _dbContext.Set<User>()
            .Where(u => u.Id == userId)
            .Select(u => new User.Builder(u.Id, u.Name).SetPassword(u.Password).Build())
            .FirstOrDefaultAsync();

        if(user is not null) _dbContext.Attach(user);

        return user;
    }
}
