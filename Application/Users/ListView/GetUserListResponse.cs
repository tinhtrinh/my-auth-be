using Application.Shared.Pagination;

namespace Application.Users.ListView;

public record GetUserListResponse : PagedResponse<object>
{
    public GetUserListResponse(
        List<object> items, 
        int? pageNumber, 
        int? pageSize, 
        int totalCount,
        int filteredCount) : base(
            items, 
            pageNumber, 
            pageSize, 
            totalCount,
            filteredCount)
    {
    }
}