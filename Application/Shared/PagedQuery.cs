namespace Application.Shared;

public record PagedQuery
{
    public string? SearchTerm;
    public string? SortColumn;
    public string? SortOrder;
    public int? PageNumber;
    public int? PageSize;

    public PagedQuery(PagedRequest request)
    {
        SearchTerm = request.SearchTerm;
        SortColumn = request.SortColumn;
        SortOrder = request.SortOrder;
        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
    }
}
