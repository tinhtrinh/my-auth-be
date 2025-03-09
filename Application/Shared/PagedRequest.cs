namespace Application.Shared;

public record PagedRequest(string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int? PageNumber,
    int? PageSize);
