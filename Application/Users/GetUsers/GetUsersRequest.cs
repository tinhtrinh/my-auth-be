using Application.Shared;

namespace Application.Users.GetUsers;

public record GetUsersRequest(string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int? PageNumber,
    int? PageSize) 
    : PagedRequest(SearchTerm,
        SortColumn, 
        SortOrder, 
        PageNumber, 
        PageSize);
