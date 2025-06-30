using Application.Shared;
using Application.Shared.Pagination;
using Domain.Users;

namespace Application.Users.GetUsers;

public record GetUsersResponse : PagedResponse<GetUsersDTO>
{
    public string? PlaceholderToTestExtra { get; private set; }

    public GetUsersResponse(
        List<GetUsersDTO> items,
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
        PlaceholderToTestExtra = "This is a placeholder to test the extension of class.";
    }
}