using Domain.Users;

namespace Domain.AuditLogs;

public partial class AuditLog
{
    private AuditLog(
        AuditLogId id,
        string action,
        string objectType,
        Guid objectId,
        UserId changedById,
        DateTime timestamp)
    {
        Id = id;
        Action = action;
        ObjectType = objectType;
        ObjectId = objectId;
        ChangedById = changedById;
        Timestamp = timestamp;
    }

    public class Builder
    {
        internal AuditLogId Id = new(Guid.Empty);

        internal bool? IsDeleted;

        internal string Action = string.Empty;

        internal string ObjectType = string.Empty;

        internal Guid ObjectId = Guid.Empty;

        internal UserId ChangedById = new(Guid.Empty);

        internal User? User;

        internal DateTime Timestamp = DateTime.UtcNow;

        internal string? PropertyName;

        internal string? OldValue;

        internal string? NewValue;

        public Builder(
            AuditLogId id, 
            string action, 
            string objectType, 
            Guid objectId, 
            UserId changedById, 
            DateTime timestamp)
        {
            Id = id;
            Action = action;
            ObjectType = objectType;
            ObjectId = objectId;
            ChangedById = changedById;
            Timestamp = timestamp;
        }

        public Builder SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
            return this;
        }

        public Builder SetUser(User user)
        {
            User = user;
            return this;
        }

        public Builder SetPropertyName(string propertyName)
        {
            PropertyName = propertyName;
            return this;
        }

        public Builder SetOldValue(string? oldValue)
        {
            OldValue = oldValue;
            return this;
        }

        public Builder SetNewValue(string? newValue)
        {
            NewValue = newValue;
            return this;
        }

        public AuditLog Build()
        {
            return new AuditLog(
                Id,
                Action,
                ObjectType,
                ObjectId,
                ChangedById,
                Timestamp)
            {
                IsDeleted = IsDeleted,
                User = User,
                PropertyName = PropertyName,
                OldValue = OldValue,
                NewValue = NewValue
            };
        }
    }
}
