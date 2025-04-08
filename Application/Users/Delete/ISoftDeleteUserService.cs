namespace Application.Users.Delete;

public interface ISoftDeleteUserService
{
    Task DeleteAsync(string id);
}
