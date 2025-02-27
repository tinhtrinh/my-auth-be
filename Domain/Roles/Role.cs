using Domain.Users;

namespace Domain.Roles;

public sealed class Role
{
    public RoleId Id { get; private set; }

    public string Name { get; private set; }

    public ICollection<Permission>? Permissions { get; private set; }

    public ICollection<User>? Users { get; private set; }

    public static Role Registered = new Role(new RoleId(new Guid()), "Registered");

    public Role(RoleId id, string name)
    {
        Id = id;
        Name = name;
    }

    public bool DoHavePermission(string permission)
    {
        if (Permissions is null || Permissions.Count == 0) return false;
        return Permissions.Any(x => x.Name == permission);
    }
}