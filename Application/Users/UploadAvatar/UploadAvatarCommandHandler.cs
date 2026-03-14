using Application.Shared.Files;
using Application.Shared.UnitOfWork;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.UploadAvatar;

internal class UploadAvatarCommandHandler : IRequestHandler<UploadAvatarCommand, Result>
{
    private readonly IUAUserRepository _uAUserRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;

    public UploadAvatarCommandHandler(IUAUserRepository uAUserRepository,
        IFileStorageService fileStorageService,
        IUnitOfWork unitOfWork)
    {
        _uAUserRepository = uAUserRepository;
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var avatarName = request.AvatarName;
        var avatarStream = request.AvatarStream;

        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure(UserError.UserNotFound);
        }

        var user = await _uAUserRepository.GetUserByIdAsync(userId);

        if(user is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        string? uploadedPath = string.Empty;
        uploadedPath = await _fileStorageService.UploadFileAsync(avatarName, avatarStream);

        if (string.IsNullOrEmpty(uploadedPath))
        {
            return Result.Failure(UserError.UploadAvatarFail);
        }

        var result = user.SetAvatarName(avatarName, uploadedPath);

        if(!result)
        {
            return Result.Failure(UserError.NoAvatar);
        }

        _uAUserRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
