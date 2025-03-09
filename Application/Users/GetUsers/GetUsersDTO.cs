namespace Application.Users.GetUsers;

public class GetUsersDTO
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Password { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime LastModifiedDate { get; private set; }

    public GetUsersDTO(Guid id, string name, string password, DateTime createdDate, DateTime lastModifiedDate)
    {
        Id = id;
        Name = name;
        Password = password;
        CreatedDate = createdDate;
        LastModifiedDate = lastModifiedDate;
    }
}