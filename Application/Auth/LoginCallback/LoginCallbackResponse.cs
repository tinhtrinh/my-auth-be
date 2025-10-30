namespace Application.Auth.LoginCallback;

public record LoginCallbackResponse(string AccessToken, string RefreshToken, string ExpiresInUtc);
