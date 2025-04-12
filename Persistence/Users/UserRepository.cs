using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Users;

public class UserRepository : Application.Users.IUserRepository
{
    private readonly MyAuthDbContext _dbContext;

    public UserRepository(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsNameUnique(string name)
    {
        return !await _dbContext.Set<User>().AnyAsync(u => u.Name.Equals(name));
    }

    public async Task<bool> IsExisted(string id)
    {
        if(Guid.TryParse(id, out Guid parsedId))
        {
            var userId = new UserId(parsedId);
            return await _dbContext.Set<User>().AnyAsync(u => u.Id == userId && u.IsDeleted != true);
        } 
        else
        {
            return false;
        }
    }

    public void Add(User user)
    {
        _dbContext.Set<User>().Add(user);
    }

    public void Update(User user)
    {
        _dbContext.Set<User>().Update(user);
    }
}

//public class UserRepository : IUserRepository
//{
//    private readonly MyAuthDbContext _dbContext;

//    public UserRepository(MyAuthDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public IQueryable<GetUsersDTO> GetUsers<GetUsersDTO>(string? searchTerm, string? sortColumn, string? sortOrder)
//    {
//        var usersQuery = _dbContext.Database
//            .SqlQuery<GetUsersDTO>($"SELECT [Id], [Name], [Password] FROM [User]");

//        if (!string.IsNullOrWhiteSpace(searchTerm))
//        {
//            //usersQuery = usersQuery.Where(u => u.Name.Contains(searchTerm));
//        }

//        if (sortOrder?.ToLower() == "desc")
//        {
//            //usersQuery = usersQuery.OrderByDescending(GetSortProperty(sortColumn));
//        }
//        else
//        {
//            //usersQuery = usersQuery.OrderBy(GetSortProperty(sortColumn));
//        }

//        //var usersList = await usersQuery
//        //    .Select(u => User.Create(u))
//        //    .ToListAsync();

//        return usersQuery;

//        //return _dbContext.Database
//        //    .SqlQuery<GetUsersDTO>($"SELECT [Id], [Name], [Password] FROM [User]");
//    }

//    private static Expression<Func<GetUsersDTO, object>> GetSortProperty(string? SortColumn) =>
//        SortColumn?.ToLower() switch
//        {
//            "name" => user => user.Name,
//            _ => user => user.Name
//        };

//    public async Task<bool> IsNameUnique(string name)
//    {
//        return !await _dbContext.Set<User>().AnyAsync(u => u.Name == name);
//    }

//    public async Task<User?> GetByName(string name)
//    {
//        return await _dbContext.Set<User>()
//            .AsNoTracking()
//            .Where(u => u.Name == name)
//            .Select(u => User.CreateWithRefreshToken(u))
//            .FirstOrDefaultAsync();
//    }

//    public async Task<User?> GetUserWithRoles(UserId userId)
//    {
//        var user = _dbContext.Set<User>()
//            .AsNoTracking()
//            .Where(u => u.Id == userId)
//            .Include(u => u.Roles!)
//            .ThenInclude(r => r.Permissions)
//            .Select(u => User.CreateWithRoles(u))
//            .FirstOrDefault();
//        return await Task.FromResult(user);
//    }

//    public async Task<User?> GetUserWithRefreshToken(UserId userId)
//    {
//        return await _dbContext.Set<User>()
//            .AsNoTracking()
//            .Where(u => u.Id == userId)
//            .Select(u => User.CreateWithRefreshToken(u))
//            .FirstOrDefaultAsync();
//    }

//    public void Add(User user)
//    {
//        _dbContext.Set<User>().Add(user);
//    }

//    public void Update(User user)
//    {
//        _dbContext.Set<User>().Update(user);
//    }
//}
