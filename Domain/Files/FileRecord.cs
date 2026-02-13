using Domain.Users;

namespace Domain.Files;

public partial class FileRecord
{
    public Guid Id { get; private set; }

    public string FileName { get; private set; }

    public string? Path { get; private set; }

    public int? Size { get; private set; }

    public DateTime? CreatedDate { get; private set; }

    public string? ContentType { get; private set; }

    public User? User { get; private set; }
}
