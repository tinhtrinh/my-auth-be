namespace Application.Auth.Refresh;

public record RefreshResponse(string AccessToken, string RefreshToken, string ExpiresInUtc);
// nên dùng response này vì sẽ có những client không lưu được vào cookie như mobile