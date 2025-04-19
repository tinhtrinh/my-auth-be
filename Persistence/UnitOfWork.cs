using Application.Abstractions;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyAuthDbContext _dbContext;

    public UnitOfWork(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        TrackEntityChangesToAuditLogs();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void TrackEntityChangesToAuditLogs()
    {
        var auditEntries = new List<AuditLog>();

        var entries = _dbContext.ChangeTracker.Entries<AuditableBase>()
            .Where(e => 
                e.State == EntityState.Added 
                || e.State == EntityState.Modified 
                || e.State == EntityState.Deleted);

        foreach(var entry in entries)
        {
            var objectType = entry.Metadata.GetTableName();
            if (objectType is null) return;

            var keyValue = GetKeyValue(entry);
            if (keyValue is null || keyValue is not Guid) return;
            var objectId = (Guid)keyValue;

            switch (entry.State)
            {
                case EntityState.Added:

                    entry.Property(e => e.CreatedDate).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.LastModifiedDate).CurrentValue = DateTime.UtcNow;

                    auditEntries.Add(
                        new AuditLog.Builder(
                            new AuditLogId(Guid.NewGuid()),
                            "Created",
                            objectType,
                            objectId,
                            new Domain.Users.UserId(new Guid("E3CFF717-B833-49BD-8B0E-3919BE992B7C")),
                            DateTime.UtcNow)
                        .SetIsDeleted(false)
                        .Build()
                    );

                    break;

                case EntityState.Deleted:

                    entry.Property(e => e.LastModifiedDate).CurrentValue = DateTime.UtcNow;

                    auditEntries.Add(
                        new AuditLog.Builder(
                            new AuditLogId(Guid.NewGuid()),
                            "Deleted",
                            objectType,
                            objectId,
                            new Domain.Users.UserId(new Guid("E3CFF717-B833-49BD-8B0E-3919BE992B7C")),
                            DateTime.UtcNow)
                        .SetIsDeleted(false)
                        .Build()
                    );

                    break;

                case EntityState.Modified:

                    entry.Property(e => e.LastModifiedDate).CurrentValue = DateTime.UtcNow;

                    var properties = entry.Properties
                        .Where(p => p.IsModified && entry.Entity.IsAuditable(p.Metadata.Name));

                    foreach(var property in properties)
                    {
                        auditEntries.Add(
                            new AuditLog.Builder(
                                new AuditLogId(Guid.NewGuid()),
                                "Modified",
                                objectType,
                                objectId,
                                new Domain.Users.UserId(new Guid("E3CFF717-B833-49BD-8B0E-3919BE992B7C")),
                                DateTime.UtcNow)
                            .SetIsDeleted(false)
                            .SetPropertyName(property.Metadata.Name)
                            .SetOldValue(property.OriginalValue?.ToString())
                            .SetNewValue(property.CurrentValue?.ToString())
                            .Build());
                    }

                    break;

                default:
                    break;
            }
        }

        if (auditEntries.Count != 0)
        {
            _dbContext.Set<AuditLog>().AddRange(auditEntries);
        }
    }

    private static object? GetKeyValue(EntityEntry entry)
    {
        var keyName = entry.Metadata.FindPrimaryKey()?
                .Properties
                .Select(p => p.Name).FirstOrDefault();
        if (keyName is null) return null;

        var keyObject = entry.Property(keyName).CurrentValue;
        if (keyObject is null) return null;

        var keyInfo = keyObject.GetType().GetProperty("Value");
        if (keyInfo is null) return null;
        return keyInfo.GetValue(keyObject);
    }

    private void ConvertDomainEventsToOutBoxMessages()
    {

    }
}