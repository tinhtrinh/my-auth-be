using Application.Abstractions;
using Application.Notifications.AddNotification;
using Domain.Notification;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly Abstractions.IUnitOfWork _unitOfWork;
    private readonly IBackgroundService _backgroundService;

    public RegisterCommandHandler(
        IUserRepository userRepository, 
        Abstractions.IUnitOfWork unitOfWork,
        IBackgroundService backgroundService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _backgroundService = backgroundService;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = User.CreateForRegistration(request.Name, request.Password);

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new RegisterResponse("test access token", "test refresh token");

        var notification = Notification.Create(
            "New User Register", 
            $"User name {newUser.Name} has registered.", 
            newUser.Id);

        _backgroundService.Enqueue<AddNotificationService>(service => service.AddNotification(notification, default));

        return Result.Success(response);
    }
}
