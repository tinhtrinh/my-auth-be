using Domain.AuditLogs;

namespace Domain.Users;

public abstract class AuditableUser : AuditableBase
{
    public override ICollection<string> AuditableProperties { get; protected set; } = new List<string>()
    {
        "Name",
        "Password"
    };
}
