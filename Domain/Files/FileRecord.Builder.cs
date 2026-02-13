namespace Domain.Files;

public partial class FileRecord
{
    private FileRecord(Guid id, string fileName)
    {
        Id = id;
        FileName = fileName;
    }

    public class Builder
    {
        internal Guid Id = Guid.Empty;

        internal string FileName = string.Empty;

        internal string? Path;

        internal string? ContentType;

        public Builder(Guid id, string fileName)
        {
            Id = id;
            FileName = fileName;
        }

        public Builder SetPath(string? path)
        {
            Path = path;
            return this;
        }

        public Builder SetContentType(string? contentType)
        {
            ContentType = contentType;
            return this;
        }

        public FileRecord Build()
        {
            return new FileRecord(Id, FileName)
            {
                Path = Path,
                ContentType = ContentType
            };
        }
    }
}
