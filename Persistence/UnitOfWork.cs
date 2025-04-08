using Application.Abstractions;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        var entries = _dbContext.ChangeTracker.Entries()
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
                    auditEntries.Add(
                        AuditLog.Create(
                            "Created", 
                            objectType,
                            objectId,
                            new Domain.Users.UserId(new Guid("E3CFF717-B833-49BD-8B0E-3919BE992B7C")),
                            null,
                            null,
                            null)
                    );
                    break;

                case EntityState.Deleted:
                    auditEntries.Add(
                        AuditLog.Create(
                            "Deleted",
                            objectType,
                            objectId,
                            new Domain.Users.UserId(new Guid("E3CFF717-B833-49BD-8B0E-3919BE992B7C")),
                            null,
                            null,
                            null)
                    );
                    break;

                case EntityState.Modified:
                    var properties = entry.Properties.Where(p => p.IsModified);
                    foreach(var property in properties)
                    {
                        auditEntries.Add(
                        AuditLog.Create(
                            "Modified",
                            objectType,
                            objectId,
                            new Domain.Users.UserId(new Guid("E3CFF717-B833-49BD-8B0E-3919BE992B7C")),
                            property.Metadata.Name,
                            property.OriginalValue?.ToString(),
                            property.CurrentValue?.ToString())
                        );
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