using Application.Users.GetUsers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Users.GetUsers;

public class GetUsersQueryService : IGetUsersQueryService
{
    private readonly MyAuthDbContext _dbContext;

    public GetUsersQueryService(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<GetUsersDTO> GetUsers(string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var usersQuery = _dbContext.Database
            .SqlQuery<GetUsersDTO>($"SELECT [Id], [Name], [Password], [CreatedDate], [LastModifiedDate] FROM [User] WHERE IsDeleted IS Not NULL AND CreatedDate IS Not NULL AND LastModifiedDate IS Not NULL");

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            usersQuery = usersQuery.Where(u => u.Name.Contains(searchTerm));
        }

        if (sortOrder?.ToLower() == "desc")
        {
            usersQuery = usersQuery.OrderByDescending(GetSortProperty(sortColumn));
        }
        else
        {
            usersQuery = usersQuery.OrderBy(GetSortProperty(sortColumn));
        }

        return usersQuery;
    }

    private static Expression<Func<GetUsersDTO, object>> GetSortProperty(string? SortColumn) =>
        SortColumn?.ToLower() switch
        {
            "name" => user => user.Name,
            "createdDate" => user => user.CreatedDate,
            "lastModifiedDate" => user => user.LastModifiedDate,
            _ => user => user.CreatedDate
        };
}
