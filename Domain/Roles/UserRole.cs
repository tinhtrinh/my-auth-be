using Domain.Users;

namespace Domain.Roles;

public class UserRole
{
    public UserId UserId { get; set; }

    public int RoleId { get; set; }

    public UserRole(UserId userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}