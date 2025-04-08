using Domain.Notifications;
using Domain.Roles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Notifications;

internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .HasConversion(
                notificationId => notificationId.Value,
                value => new NotificationId(value));

        builder.Property(n => n.UserId)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value));
    }
}
