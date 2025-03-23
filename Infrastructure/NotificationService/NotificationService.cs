using Application.Abstractions;
using Application.Notifications;
using Domain.Notification;

namespace Infrastructure.NotificationService;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task SendNotification(Notification notification, CancellationToken cancellationToken)
    {
        _notificationRepository.Add(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
