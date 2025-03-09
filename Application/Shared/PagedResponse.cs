using Microsoft.EntityFrameworkCore;

namespace Application.Shared;

public class PagedResponse<TDTO>
{
    public List<TDTO> Items { get; private set; } = [];

    public int PageNumber { get; private set; }

    public int TotalPages { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public bool HasPrevious => PageNumber > 1;

    public bool HasNext => PageNumber < TotalPages;

    private const int DefaultPageNumber = 1;

    private const int DefaultPageSize = 50;

    protected PagedResponse() { }

    protected static async Task<TChild> CreateChildAsync<TChild>(
        IQueryable<TDTO> query, 
        int? pageNumber, 
        int? pageSize)
        where TChild : PagedResponse<TDTO>, new()
    {
        var _totalCount = await query.CountAsync();
        var _pageNumber = pageNumber ?? DefaultPageNumber;
        var _pageSize = pageSize ?? DefaultPageSize;
        var _totalPages = (int)Math.Ceiling(_totalCount / (double)_pageSize);
        var _items = await query.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize).ToListAsync();

        return new TChild()
        {
            TotalCount = _totalCount,
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            TotalPages = _totalPages,
            Items = _items
        };
    }
}