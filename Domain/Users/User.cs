using Domain.AuditLogs;
using Domain.Notifications;
using Domain.Roles;
using Domain.Shared;

namespace Domain.Users;

public class User
{
    public UserId Id { get; private set; }

    public bool IsDeleted { get; private set; }

    public string Name { get; private set; }

    public string? Password { get; private set; }

    public DateTime? CreatedDate { get; private set; }

    public UserId? CreatedById { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public UserId? LastModifiedById { get; private set; }

    public RefreshToken? RefreshToken { get; private set; }

    public ICollection<Role>? Roles { get; private set; }

    public ICollection<Notification>? Notifications { get; private set; }

    public ICollection<AuditLog>? AuditLogs { get; private set; }

    private User()
    {
        Id = new UserId(Guid.NewGuid());
        IsDeleted = false;
        Name = "";
    }

    public static User CreateForRegistration(string name, string password)
    {
        var newUserId = new UserId(Guid.NewGuid());

        return new User()
        {
            Id = newUserId,
            Name = name,
            Password = password,
            CreatedDate = DateTime.UtcNow,
            CreatedById = newUserId,
            LastModifiedDate = DateTime.UtcNow
        };
    }

    //private User(UserId id, string name, string password)
    //{
    //    Id = id;
    //    Name = "";
    //    Password = "";
    //}

    //public static User Create(UserId id, string name, string password, RefreshToken? refreshToken = null, List<Role>? roles = null)
    //{
    //    return new User(id, name, password)
    //    {
    //        RefreshToken = refreshToken,
    //        Roles = roles
    //    };
    //}

    //public static User Create(User user)
    //{
    //    return new User(user.Id, user.Name, user.Password);
    //}

    //public static User CreateWithRoles(User user)
    //{
    //    return new User(user.Id, user.Name, user.Password)
    //    {
    //        Roles = user.Roles
    //    };
    //}

    //public static User CreateWithRefreshToken(User user)
    //{
    //    return new User(user.Id, user.Name, user.Password)
    //    {
    //        RefreshToken = user.RefreshToken
    //    };
    //}

    public Result AddRefreshToken(string refreshTokenValue)
    {
        if (refreshTokenValue == string.Empty)
        {
            return Result.Failure(UserError.AddRefreshTokenFailed);
        }
        RefreshToken = RefreshToken.Create(refreshTokenValue);
        return Result.Success();
    }

    public Result UpdateRefreshToken(string refreshTokenValue)
    {
        if (RefreshToken is null || refreshTokenValue == string.Empty)
        {
            return Result.Failure(UserError.AddRefreshTokenFailed);
        }
        RefreshToken.UpdateValue(refreshTokenValue);
        return Result.Success();
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
    }

    public bool IsRefreshTokenNotNull()
    {
        return RefreshToken != null;
    }

    public bool IsRefreshTokenEqual(string refreshTokenValue)
    {
        if (RefreshToken is null) return false;
        return RefreshToken.IsRefreshTokenEqual(refreshTokenValue);
    }

    public bool IsRefreshTokenExpired()
    {
        if (RefreshToken is null) return false;
        return RefreshToken.IsRefreshTokenExpired();
    }

    public bool DoHavePermission(string permission)
    {
        if (Roles is null) return false;
        return Roles.Any(r => r.DoHavePermission(permission));
    }
}