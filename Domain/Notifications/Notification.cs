using Domain.Users;

namespace Domain.Notifications;

public class Notification
{
    public NotificationId Id { get; private set; }

    public bool IsDeleted { get; private set; }

    public string Title { get; private set; }

    public string Message { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public UserId UserId { get; private set; }

    public User? User { get; private set; }

    public Notification(NotificationId id, string title, string message, DateTime createdDate, UserId userId)
    {
        Id = id;
        IsDeleted = false;
        Title = title;
        Message = message;
        CreatedDate = createdDate;
        UserId = userId;
    }
}
