using Application.Shared.Notifications;
using Application.Shared.RealTime;
using Application.Shared.UnitOfWork;
using Domain.Notifications;

namespace Application.Notifications.NotificationService;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRealTimeService _realTimeService;

    public NotificationService(
        INotificationRepository notificationRepository,
        IUnitOfWork unitOfWork,
        IRealTimeService realTimeService)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _realTimeService = realTimeService;
    }

    public async Task SendAsync(Notification notification, CancellationToken cancellationToken)
    {
        _notificationRepository.Add(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _realTimeService.SendAsync("SendNotification", notification, cancellationToken); ;
    }
}
