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
}