using Domain.Shared;

namespace Domain.Users;

public static class UserError
{
    public static readonly Error UserNotFound = new("User.NotFound", "User Not Found");

    public static readonly Error NotUniqueName = new("User.NotUniqueName", "Name must be unique");

    public static readonly Error InvalidAccessToken = new("User.InvalidAccessToken", "Invalid Access Token");

    public static readonly Error InvalidRefreshToken = new("User.InvalidRefreshToken", "Invalid Refresh Token");

    public static readonly Error GenerateAccessTokenFailed = new("User.GenerateAccessTokenFailed", "Generate Access Token Failed");

    public static readonly Error AddRefreshTokenFailed = new("User.AddRefreshTokenFailed", "Add Refresh Token Failed");

    public static readonly Error AddUserFailed = new("User.AddUserFailed", "Add User Failed");

    public static readonly Error NoName = new("UserRule.NoName", "Name is required");

    public static Error ShortName(int? minNameLength) => new("UserRule.ShortName", $"Name minimum length is {minNameLength}");

    public static readonly Error NoPassword = new("UserRule.NoPassword", "Password is required");

    public static Error ShortPassword(int? minPasswordLength) => new("UserRule.ShortPassword", $"Password minimum length is {minPasswordLength}");

    public static readonly Error RefreshTokenNotFound = new("RefreshToken.NotFound", "Refresh Token Not Found");

    public static readonly Error LogoutFail = new("RefreshToken.NotFound", "Refresh Token Not Found");

    public static readonly Error GetJwtFail = new("Jwt.GetFail", "Get JWT fail");

    public static readonly Error NoRole = new("User.NoRole", "User has no role.");

    public static readonly Error NoRequirePermission = new("User.NoRequirePermission", "User has no require permission.");

    public static readonly Error NoAvatar = new("User.NoAvatar", "User has no avatar.");
}