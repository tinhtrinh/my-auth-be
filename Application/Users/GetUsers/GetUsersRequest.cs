using Application.Shared.Pagination;

namespace Application.Users.GetUsers;

public record GetUsersRequest(
    string? SearchTerm,
    string? SearchColumn,
    string? SortColumn,
    string? SortOrder,
    int? PageNumber,
    int? PageSize) 
    : PagedRequest(
        SearchTerm,
        SearchColumn,
        SortColumn, 
        SortOrder, 
        PageNumber, 
        PageSize);
