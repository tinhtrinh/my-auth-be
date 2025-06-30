using Application.Shared.Pagination;

namespace Application.Users.GetUsers;

public interface IGetUsersQueryService : ICountDataService
{
    Task<List<GetUsersDTO>> GetUsers(string? searchTerm, string? sortColumn, string? sortOrder);
}
