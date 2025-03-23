using Application.Abstractions;
using Application.Notifications;
using Domain.Notification;
using Domain.Shared;
using Domain.Users;
using Hangfire;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Users.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly Abstractions.IUnitOfWork _unitOfWork;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public RegisterCommandHandler(
        IUserRepository userRepository, 
        Abstractions.IUnitOfWork unitOfWork, 
        IBackgroundJobClient backgroundJobClient)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = User.CreateForRegistration(request.Name, request.Password);

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new RegisterResponse("test access token", "test refresh token");

        var userName = newUser.Name;
        var userId = newUser.Id;

        var notification = Notification.Create(
                "New User Register",
                "User name test no user name"
                + newUser.Name 
                +
                " has registered.",
                newUser.Id
                );

        _backgroundJobClient.Enqueue<INotificationService>(service => service.SendNotification(notification, default));

        return Result.Success(response);
    }
}
