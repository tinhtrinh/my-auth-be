using Application.ListViews.Shared;

namespace Application.Users.ListView;

public interface IGetUserListService
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

    Task<int> CountAsync();

    Task<int> FilteredCountAsync();
}
