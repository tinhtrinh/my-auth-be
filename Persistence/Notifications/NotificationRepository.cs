using Application.Notifications;
using Domain.Notifications;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Notifications;

public class NotificationRepository : INotificationRepository
{
    private readonly MyAuthDbContext _dbContext;

    public NotificationRepository(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Notification notification)
    {
        _dbContext.Set<Notification>().Add(notification);
    }
}
