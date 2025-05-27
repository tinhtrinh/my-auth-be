namespace Application.Shared.Pagination;

public abstract record PagedResponse<TData>
{
    public List<TData> Items { get; init; } = [];

    public int PageNumber { get; init; }

    public int TotalPages { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public int FilteredCount { get; init; }

    public bool HasPrevious => PageNumber > 1;

    public bool HasNext => PageNumber < TotalPages;

    private const int DefaultPageNumber = 1;

    private const int DefaultPageSize = 50;

    protected PagedResponse(List<TData> items, int? pageNumber, int? pageSize, int totalCount, int filteredCount)
    {
        if(pageNumber <= 0)
        {
            throw new ArgumentException("PageNumber must be greater than 0");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException("PageSize must be greater than 0");
        }

        Items = items;
        PageNumber = pageNumber ?? DefaultPageNumber;
        PageSize = pageSize ?? DefaultPageSize;
        TotalCount = totalCount;
        FilteredCount = filteredCount;

        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
