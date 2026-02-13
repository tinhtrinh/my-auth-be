using Application.Shared.Files;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.DownloadAvatar;

internal class DownloadAvatarQueryHandler : IRequestHandler<DownloadAvatarQuery, Result<DownloadAvatarResponse>>
{
    private readonly IDAUserService _dAUserService;
    private readonly IFileService _fileService;

    public DownloadAvatarQueryHandler(IDAUserService dAUserService, IFileService fileService)
    {
        _dAUserService = dAUserService;
        _fileService = fileService;
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

        var stream = await _fileService.GetFileStreamAsync(avatar.Id);

        if (stream is null)
        {
            return Result.Failure<DownloadAvatarResponse>(UserError.NoAvatar);
        }

        if (string.IsNullOrEmpty(avatar.ContentType))
        {
            return Result.Failure<DownloadAvatarResponse>(UserError.NoAvatar);
        }

        var response = new DownloadAvatarResponse(stream, avatar.ContentType, avatar.FileName);

        return Result.Success(response);
    }
}
