using Domain.Users;

namespace Domain.AuditLogs;

public partial class AuditLog
{
    public AuditLogId Id { get; private set; }

    public bool? IsDeleted { get; private set; }

    public string Action { get; private set; }

    public string ObjectType { get; private set; }

    public Guid ObjectId { get; private set; }

    public UserId ChangedById { get; private set; }

    public User? User { get; private set; }

    public DateTime Timestamp { get; private set; }

    public string? PropertyName { get; private set; }

    public string? OldValue { get; private set; }

    public string? NewValue { get; private set; }
}
