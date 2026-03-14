using Domain.Shared;
using MediatR;

namespace Application.Users.UploadAvatar;

public record UploadAvatarCommand(string? UserId, string AvatarName, Stream AvatarStream) : IRequest<Result>;
