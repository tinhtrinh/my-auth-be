using Domain.Users;

namespace Domain.AuditLog;

public class AuditLog
{
    public AuditLogId Id { get; private set; }

    public string Action { get; private set; }

    public string ObjectType { get; private set; }

    public Guid ObjectId { get; private set; }

    public UserId ChangedById { get; private set; }

    public User? User { get; private set; }

    public DateTime Timestamp { get; private set; }

    public string? PropertyName { get; private set; } = null;

    public string? OldValue { get; private set; }

    public string? NewValue { get; private set; }

    private AuditLog(Guid objectId, UserId changedById)
    {
        Id = new AuditLogId(Guid.NewGuid());
        Action = "";
        ObjectType = "";
        ObjectId = objectId;
        ChangedById = changedById;
        Timestamp = DateTime.UtcNow;
    }

    public static AuditLog Create(
        string action,
        string objectType,
        Guid objectId,
        UserId changedById, 
        string? propertyName,
        string? oldValue, 
        string? newValue)
    {
        return new AuditLog(objectId, changedById)
        {
            Action = action,
            ObjectType = objectType,
            PropertyName = propertyName,
            OldValue = oldValue,
            NewValue = newValue
        };
    }
}
