using Domain.UserRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users;

public static class UserRule
{
    public static readonly int MinNameLength = 3;

    public static readonly string InvalidName = "invalidName";

    public static int MinPasswordLength = 8;

    public static bool IsUserNameLengthValid(string userName)
    {
        return userName.Length >= MinNameLength;
    }

    public static bool IsNameValid(string name)
    {
        return name == InvalidName;
    }

    public static bool IsPasswordLengthValid(string password)
    {
        return password.Length >= MinPasswordLength;
    }
}
