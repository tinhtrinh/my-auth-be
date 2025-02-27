namespace Domain.UserRules;

public class UserRule
{
    public UserRuleId Id { get; set; }

    public int MinNameLength { get; set; }

    public string InvalidName { get; set; }

    public int MinPasswordLength { get; set; }

    public UserRule(UserRuleId id, int minNameLength, string invalidName, int minPasswordLength)
    {
        Id = id;
        MinNameLength = minNameLength;
        InvalidName = invalidName;
        MinPasswordLength = minPasswordLength;
    }

    public bool IsUserNameLengthValid(string userName)
    {
        return userName.Length >= MinNameLength;
    }

    public bool IsNameValid(string name)
    {
        return name == InvalidName;
    }

    public bool IsPasswordLengthValid(string password)
    {
        return password.Length >= MinPasswordLength;
    }
}