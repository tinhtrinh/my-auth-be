using Domain.Shared;

namespace Domain.UserRules;

public static class UserRuleError
{
    public static readonly Error UserRuleNotFound = new("UserRule.NotFound", "User Rule Not Found");

    public static readonly Error RegisterCommandNotFound = new("UserRule.RegisterCommandNotFound", "Register Command Not Found");

    public static readonly Error LoginCommandNotFound = new("UserRule.LoginCommandNotFound", "Login Command Not Found");

    public static readonly Error NoName = new("UserRule.NoName", "Name is required");

    public static Error ShortName(int? minNameLength) => new("UserRule.ShortName", $"Name minimum length is {minNameLength}");

    public static readonly Error NoPassword = new("UserRule.NoPassword", "Password is required");

    public static Error ShortPassword(int? minPasswordLength) => new("UserRule.ShortPassword", $"Password minimum length is {minPasswordLength}");

};