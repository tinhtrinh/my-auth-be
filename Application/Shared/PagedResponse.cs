namespace Application.Shared;

public abstract class PagedResponse<T>
{
    public List<T> Items { get; private set; }

    public int PageNumber { get; private set; }

    public int TotalPages { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public bool HasPrevious => PageNumber > 1;

    public bool HasNext => PageNumber < TotalPages;

    private const int DefaultPageNumber = 1;

    private const int DefaultPageSize = 50;

    protected PagedResponse(IEnumerable<T> query, int? pageNumber, int? pageSize)
    {
        TotalCount = query.Count();
        PageNumber = pageNumber ?? DefaultPageNumber;
        PageSize = pageSize ?? DefaultPageSize;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        Items = query.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
    }
}