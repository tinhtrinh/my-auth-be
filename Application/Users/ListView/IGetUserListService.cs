using Application.Shared.ListView;
using Application.Shared.Pagination;

namespace Application.Users.ListView;

public interface IGetUserListService : ICountDataService
{
    Task<List<object>> GetUserListAsync(
        List<string> columns,
        List<FilterCondition>? filter,
        string? searchTerm,
        string? searchColumn,
        string? sortColumn,
        string? sortOrder,
        int? pageNumber,
        int? pageSize);
}
