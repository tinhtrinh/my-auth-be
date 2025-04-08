using Application.Abstractions;
using Application.Shared;
using Domain.Notifications;

namespace Application.Notifications.AddNotification;

public class AddNotificationService : IAddNotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRealTimeNotifier _realTimeNotifier;

    public AddNotificationService(
        INotificationRepository notificationRepository, 
        IUnitOfWork unitOfWork,
        IRealTimeNotifier realTimeNotifier)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _realTimeNotifier = realTimeNotifier;
    }

    public async Task AddNotification(Notification notification, CancellationToken cancellationToken)
    {
        _notificationRepository.Add(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _realTimeNotifier.Notify(notification);
    }
}
