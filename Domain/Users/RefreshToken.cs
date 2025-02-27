namespace Domain.Users;

public record RefreshToken
{
    public string Value { get; private set; }

    public DateTime ExpiryTime { get; private set; }

    private const int DEFAULT_EXPIRY_TIME = 60;

    private RefreshToken(string value, DateTime expiryTime)
    {
        Value = value;
        ExpiryTime = expiryTime;
    }

    public static RefreshToken Create(string value)
    {
        DateTime expiryTime = DateTime.Now.AddMinutes(DEFAULT_EXPIRY_TIME);
        return new RefreshToken(value, expiryTime);
    }

    internal void UpdateValue(string value)
    {
        Value = value;
    }

    internal bool IsRefreshTokenEqual(string value)
    {
        return Value == value;
    }

    internal bool IsRefreshTokenExpired()
    {
        return DateTime.Now >= ExpiryTime;
    }
}