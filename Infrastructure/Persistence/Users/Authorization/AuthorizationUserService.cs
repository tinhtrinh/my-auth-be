using Domain.Permissions;
using Domain.Roles;
using Domain.Users;
using Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Users.Authorization;

public class AuthorizationUserService : IAuthorizationUserService
{
    private readonly MyAuthDbContext _dbContext;

    public AuthorizationUserService(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserWithRoles(UserId id)
    {
        var user = await _dbContext.Set<User>()
           .AsNoTracking()
           .Where(u => u.IsDeleted != true && u.Id == id)
           .Select(
               u => new User.Builder(u.Id, u.Name)
                   .SetRoles(
                       u.Roles != null ?
                           u.Roles.Where(r => r.IsDeleted != true)
                                .Select(
                                    r => new Role.Builder(r.Id, r.Name)
                                        .SetPermissions(
                                            r.Permissions != null ?
                                                r.Permissions.Select(p => new Permission(p.Id, p.Name))
                                                    .ToList()
                                                : null
                                        )
                                        .Build()
                                )
                                .ToList()
                           : null
                   )
                   .Build()
           )
           .FirstOrDefaultAsync();

        return user;
    }
}
