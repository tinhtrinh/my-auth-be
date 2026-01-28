using Domain.Permissions;
using Domain.Users;

namespace Domain.Roles;

public sealed partial class Role
{
    public RoleId Id { get; private set; }

    public bool? IsDeleted { get; private set; }

    public string Name { get; private set; }

    public ICollection<Permission>? Permissions { get; private set; }

    public ICollection<User>? Users { get; private set; }

    public static Role Registered = new Role(new RoleId(new Guid()), "Registered");

    public bool HasPermission(string permission)
    {
        if (Permissions is null) return false;
        return Permissions.Any(x => x.Name == permission);
    }
}