using Domain.Roles;

namespace Domain.Permissions;

public class Permission
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public ICollection<Role>? Roles { get; private set; }

    public Permission(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}