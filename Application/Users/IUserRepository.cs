using Application.Users.GetUsers;
using Domain.Users;
namespace Application.Users;

public interface IUserRepository
{
    IQueryable<GetUsersDTO> GetUsers(string? searchTerm, string? sortColumn, string? sortOrder);

    Task<bool> IsNameUnique(string name);

    void Add(User user);
}
