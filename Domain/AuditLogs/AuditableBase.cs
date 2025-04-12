using Domain.Users;

namespace Domain.AuditLogs;

public abstract class AuditableBase
{
    public UserId? CreatedById { get; protected set; }

    public DateTime? CreatedDate { get; protected set; }

    public UserId? LastModifiedById { get; protected set; }

    public DateTime? LastModifiedDate { get; protected set; }

    public abstract ICollection<string> AuditableProperties { get; protected set; }

    public bool IsAuditable(string propertyName)
    {
        return AuditableProperties.Any(p => p == propertyName);
    }
}
