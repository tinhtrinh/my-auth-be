using Application.Users.GetUsers;
namespace Application.Users;

public interface IUserRepository
{
    IQueryable<GetUsersDTO> GetUsers(string? searchTerm, string? sortColumn, string? sortOrder);
}
