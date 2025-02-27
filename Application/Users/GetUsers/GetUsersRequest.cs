namespace Application.Users.GetUsers;

public record GetUsersRequest(string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int? PageNumber,
    int? PageSize);