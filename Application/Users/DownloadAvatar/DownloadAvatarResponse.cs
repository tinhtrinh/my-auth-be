namespace Application.Users.DownloadAvatar;

public record DownloadAvatarResponse(Stream FileStream, string ContentType, string FileName);
