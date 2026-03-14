using Microsoft.AspNetCore.Http;

namespace Presentation.User.UploadAvatar;

public record UploadAvatarRequest(IFormFile AvatarFile, string? Description);
