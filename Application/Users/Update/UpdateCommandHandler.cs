using Application.Shared.Background;
using Application.Shared.Notifications;
using Application.Shared.UnitOfWork;
using Domain.Notifications;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.Update;

internal sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result>
{
    private readonly IUpdateUserFinder _updateUserFinder;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundService _backgroundService;

    public UpdateCommandHandler(
        IUpdateUserFinder updateUserFinder,
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork,
        IBackgroundService backgroundService)
    {
        _updateUserFinder = updateUserFinder;
        _unitOfWork = unitOfWork;
        _backgroundService = backgroundService;
    }

    public async Task<Result> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _updateUserFinder.FindAsync(request.Id);
        if(user is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        var result = user.ChangeNameAndPassword(request.Name, request.Password);
        if (result.IsFailure) return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var notification = new Notification(
            new NotificationId(Guid.NewGuid()),
            "User Updated",
            $"User name {user.Name} has been updated.",
            DateTime.UtcNow,
            user.Id);

        _backgroundService.Enqueue<INotificationService>(service => service.SendAsync(notification, cancellationToken));

        return Result.Success();
    }
}
