namespace Application.Shared.Pagination;

public abstract record PagedRequest
{
    public string? SearchTerm { get; init; }

    public string? SearchColumn { get; init; }

    public string? SortColumn { get; init; }

    public string? SortOrder { get; init; }

    public int? PageNumber { get; init; }

    public int? PageSize { get; init; }

    private const int DefaultPageNumber = 1;

    private const int DefaultPageSize = 50;

    protected PagedRequest(
        string? searchTerm, 
        string? searchColumn, 
        string? sortColumn, 
        string? sortOrder, 
        int? pageNumber, 
        int? pageSize)
    {
        if (pageNumber <= 0)
        {
            throw new ArgumentException("PageNumber must be greater than 0");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException("PageSize must be greater than 0");
        }

        SearchTerm = searchTerm;
        SearchColumn = searchColumn;
        SortColumn = sortColumn;
        SortOrder = sortOrder;
        PageNumber = pageNumber ?? DefaultPageNumber;
        PageSize = pageSize ?? DefaultPageSize;
    }
}
