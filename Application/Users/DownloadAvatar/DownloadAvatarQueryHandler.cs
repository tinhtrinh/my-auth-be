using Application.Shared.Files;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.DownloadAvatar;

internal class DownloadAvatarQueryHandler : IRequestHandler<DownloadAvatarQuery, Result<DownloadAvatarResponse>>
{
    private readonly IDAUserService _dAUserService;
    private readonly IFileStorageService _fileStorageService;

    public DownloadAvatarQueryHandler(IDAUserService dAUserService, IFileStorageService fileStorageService)
    {
        _dAUserService = dAUserService;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<DownloadAvatarResponse>> Handle(DownloadAvatarQuery request, CancellationToken cancellationToken)
    {

        if (!Guid.TryParse(request.UserId, out Guid userGuid))
        {
            return Result.Failure<DownloadAvatarResponse>(UserError.UserNotFound);
        }

        var userId = new UserId(userGuid);

        var avatar = await _dAUserService.GetUserAvatarAsync(userId);

        if(avatar is null)
        {
            return Result.Failure<DownloadAvatarResponse>(UserError.NoAvatar);
        }

        var stream = await _fileStorageService.GetFileStreamAsync(avatar.FileName);

        if (stream is null || string.IsNullOrEmpty(avatar.ContentType))
        {
            return Result.Failure<DownloadAvatarResponse>(UserError.NoAvatar);
        }

        var response = new DownloadAvatarResponse(stream, avatar.ContentType, avatar.FileName);

        return Result.Success(response);
    }
}
