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

        public User Build()
        {
            return new User(Id, Name)
            {
                IsDeleted = IsDeleted,
                Password = Password
            };
        }
    }
}
