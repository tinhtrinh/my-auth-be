using Domain.Permissions;

namespace Domain.Roles;

public partial class Role
{
    private Role(RoleId id, string name)
    {
        Id = id;
        Name = name;
    }

    public class Builder
    {
        internal RoleId Id = new(Guid.Empty);

        internal bool? IsDeleted;

        internal string Name = "";

        internal ICollection<Permission>? Permissions;

        public Builder(RoleId id, string name)
        {
            Id = id;
            Name = name;
        }

        public Builder SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
            return this;
        }

        public Builder SetPermissions(ICollection<Permission>? permissions)
        {
            Permissions = permissions;
            return this;
        }

        public Role Build()
        {
            return new Role(Id, Name)
            {
                IsDeleted = IsDeleted,
                Permissions = Permissions
            };
        }
    }
}
