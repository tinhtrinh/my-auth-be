using Domain.UserRules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.UserRules;

internal class UserRuleConfiguration : IEntityTypeConfiguration<UserRule>
{
    public void Configure(EntityTypeBuilder<UserRule> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                userRuleId => userRuleId.Value,
                value => new UserRuleId(value));
    }
}