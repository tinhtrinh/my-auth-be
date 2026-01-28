using Domain.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Permissions;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
