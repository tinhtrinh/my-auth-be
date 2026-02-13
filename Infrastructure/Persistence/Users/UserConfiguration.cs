using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value));

        builder.Property(u => u.CreatedById)
            .HasConversion(
                createdById => createdById != null ? createdById.Value : (Guid?)null,
                value => value.HasValue ? new UserId(value.Value) : null);

        builder.Property(u => u.LastModifiedById)
            .HasConversion(
                lastModifiedById => lastModifiedById != null ? lastModifiedById.Value : (Guid?)null,
                value => value.HasValue ? new UserId(value.Value) : null);

        builder.OwnsOne(u => u.RefreshToken, refreshTokenBuilder =>
        {
            refreshTokenBuilder.Property(r => r.Value);
        });

        builder.HasIndex(u => u.Id);

        builder.HasIndex(u => u.Name);

        builder.HasIndex(u => u.CreatedDate);

        builder.HasMany(u => u.Notifications)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AuditLogs)
           .WithOne(n => n.User)
           .HasForeignKey(n => n.ChangedById)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(u => u.AuditableProperties);

        builder.HasOne(u => u.Avatar)
            .WithOne(fr => fr.User)
            .HasForeignKey<User>(u => u.AvatarId);
    }
}