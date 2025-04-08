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

    private Notification(UserId userId)
    {
        Id = new NotificationId(Guid.NewGuid());
        Title = "";
        Message = "";
        CreatedDate = DateTime.UtcNow;
        UserId = userId;
    }

    public static Notification Create(string title, string message, UserId userId)
    {
        return new Notification(userId)
        {
            Title = title,
            Message = message
        };
    }
}
