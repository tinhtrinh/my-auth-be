using Application.Shared.Pagination;

namespace Application.ListViews.Shared;

public record GetListDataRequest : PagedRequest
{
    public List<string> Columns { get; init; }

    public List<FilterCondition>? Filter { get; init; }

    public GetListDataRequest(
        List<string> columns,
        List<FilterCondition>? filter,
        string? SearchTerm,
        string? SearchColumn,
        string? SortColumn, 
        string? SortOrder, 
        int? PageNumber, 
        int? PageSize) : base(
            SearchTerm,
            SearchColumn,
            SortColumn, 
            SortOrder, 
            PageNumber, 
            PageSize)
    {
        if (columns.Count == 0)
        {
            throw new ArgumentException("Columns must not be empty.");
        }

        Columns = columns;
        Filter = filter;
    }
}
