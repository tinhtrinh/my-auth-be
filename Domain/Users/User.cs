using Domain.AuditLogs;
using Domain.Files;
using Domain.Notifications;
using Domain.Roles;
using Domain.Shared;

namespace Domain.Users;

public partial class User : AuditableUser
{
    public UserId Id { get; private set; }

    public bool? IsDeleted { get; private set; }

    public string Name { get; private set; }

    public string? Password { get; private set; }

    public RefreshToken? RefreshToken { get; private set; }

    public ICollection<Role>? Roles { get; private set; }

    public ICollection<Notification>? Notifications { get; private set; }

    public ICollection<AuditLog>? AuditLogs { get; private set; }

    public Guid? AvatarId { get; private set; }

    public FileRecord? Avatar { get; private set; }

    public Result ChangeNameAndPassword(string name, string password)
    {
        if(name.Length < UserRule.MinNameLength)
        {
            return Result.Failure(UserError.ShortName(UserRule.MinNameLength));
        }

        if (password.Length < UserRule.MinPasswordLength)
        {
            return Result.Failure(UserError.ShortName(UserRule.MinNameLength));
        }

        Name = name;
        Password = password;
        return Result.Success();
    }

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

    public bool HasPermission(string permission)
    {
        if (Roles is null) return false;
        return Roles.Any(r => r.HasPermission(permission));
    }

    public bool HasNoRole()
    {
        return Roles is null || Roles.Count == 0;
    }
}