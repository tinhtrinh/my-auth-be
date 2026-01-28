using Domain.Roles;

namespace Domain.Users;

public partial class User
{
    private User(UserId id, string name)
    {
        Id = id;
        Name = name;
    }

    public class Builder
    {
        internal UserId Id = new(new Guid());

        internal bool? IsDeleted;

        internal string Name = "";

        internal string? Password;

        internal ICollection<Role>? Roles;

        public Builder(UserId id, string name)
        {
            Id = id;
            Name = name;
        }

        public Builder SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
            return this;
        }

        public Builder SetPassword(string? password)
        {
            Password = password;
            return this;
        }

        public Builder SetRoles(ICollection<Role>? roles)
        {
            Roles = roles;
            return this;
        }

        public User Build()
        {
            return new User(Id, Name)
            {
                IsDeleted = IsDeleted,
                Password = Password,
                Roles = Roles
            };
        }
    }
}
