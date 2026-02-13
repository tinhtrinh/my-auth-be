using Domain.Files;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.FileRecords;

public class FileRecordConfiguration : IEntityTypeConfiguration<FileRecord>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FileRecord> builder)
    {
        builder.HasKey(fr => fr.Id);
    }
}
