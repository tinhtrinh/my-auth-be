using Application.Shared.Background;
using Application.Shared.Notifications;
using Application.Shared.UnitOfWork;
using Domain.Notifications;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundService _backgroundService;

    public RegisterCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork,
        IBackgroundService backgroundService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _backgroundService = backgroundService;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User.Builder(new UserId(Guid.NewGuid()), request.Name)
            .SetPassword(request.Password)
            .SetIsDeleted(false)
            .Build();

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new RegisterResponse("test access token", "test refresh token");

        var notification = new Notification(
            new NotificationId(Guid.NewGuid()),
            "New User Register",
            $"User name {newUser.Name} has registered.",
            DateTime.UtcNow,
            newUser.Id);

        _backgroundService.Enqueue<INotificationService>(service => service.SendAsync(notification, cancellationToken));

        return Result.Success(response);
    }
}
