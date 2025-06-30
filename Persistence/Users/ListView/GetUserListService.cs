using Application.Shared.ListView;
using Application.Users.ListView;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Persistence.Users.ListView;

public class GetUserListService : IGetUserListService
{
    private readonly MyAuthDbContext _dbContext;
    private IQueryable<object> query;

    public GetUserListService(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
        query = dbContext.Set<User>();
    }

    public async Task<int> CountAsync()
    {
        return await _dbContext.Set<User>().Where(u => u.IsDeleted != true).CountAsync();
    }

    public async Task<int> FilteredCountAsync()
    {
        return await query.CountAsync();
    }

    public async Task<List<object>> GetUserListAsync(List<string> columns, List<FilterCondition>? filter, string? searchTerm, string? searchColumn, string? sortColumn, string? sortOrder, int? pageNumber, int? pageSize)
    {
        query = (IQueryable<object>)_dbContext.Set<User>()
           .Select("new (Name, CreatedDate)")
            .Where("!Name.Contains(\"string\")");

        var users = await query.ToDynamicListAsync();
        return users;
    }
}
