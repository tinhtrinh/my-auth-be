using System.Collections;

namespace Domain.Users;

public interface IUserRepository
{
    IQueryable<TDTO> GetUsers<TDTO>(string? searchTerm, string? sortColumn, string? sortOrder);

    Task<bool> IsNameUnique(string name);

    Task<User?> GetByName(string name);

    Task<User?> GetUserWithRoles(UserId userId);

    Task<User?> GetUserWithRefreshToken(UserId userId);

    void Add(User user);

    void Update(User user);
}