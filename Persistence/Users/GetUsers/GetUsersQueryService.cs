using Application.Users.GetUsers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Users.GetUsers;

public class GetUsersQueryService : IGetUsersQueryService
{
    private readonly MyAuthDbContext _dbContext;
    private IQueryable<object>? _usersQuery;

    public GetUsersQueryService(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
        _usersQuery = null;
    }

    public async Task<List<GetUsersDTO>> GetUsers(string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var usersQuery = _dbContext.Database
            .SqlQuery<GetUsersDTO>($"SELECT [Id], [Name], [Password], [CreatedDate], [LastModifiedDate] FROM [User] WHERE IsDeleted IS Not NULL AND CreatedDate IS Not NULL AND LastModifiedDate IS Not NULL");

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            usersQuery = usersQuery.Where(u => u.Name.Contains(searchTerm));
            _usersQuery = usersQuery;
        }

        if (sortOrder?.ToLower() == "desc")
        {
            usersQuery = usersQuery.OrderByDescending(GetSortProperty(sortColumn));
        }
        else
        {
            usersQuery = usersQuery.OrderBy(GetSortProperty(sortColumn));
        }

        return await usersQuery.ToListAsync();
    }

    private static Expression<Func<GetUsersDTO, object>> GetSortProperty(string? SortColumn) =>
        SortColumn?.ToLower() switch
        {
            "name" => user => user.Name,
            "createdDate" => user => user.CreatedDate,
            "lastModifiedDate" => user => user.LastModifiedDate,
            _ => user => user.CreatedDate
        };

    public async Task<int> CountAsync()
    {
        return await _dbContext.Set<User>().Where(u => u.IsDeleted != true).CountAsync();
    }

    public async Task<int> FilteredCountAsync()
    {
        if (_usersQuery is null) return 0;
        return await _usersQuery.CountAsync();
    }
}
