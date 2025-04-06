using Domain.AuditLog;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.AuditLogs;

internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                auditLogId => auditLogId.Value,
                value => new AuditLogId(value));

        builder.Property(a => a.ChangedById)
            .HasConversion(
                changedById => changedById.Value,
                value => new UserId(value));

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.ChangedById)
            .IsRequired();
    }
}
