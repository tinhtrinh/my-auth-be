using Domain.Shared;
using MediatR;

namespace Application.Users.DownloadAvatar;

public record DownloadAvatarQuery(string UserId) : IRequest<Result<DownloadAvatarResponse>>;
