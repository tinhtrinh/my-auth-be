
namespace Infrastructure.Files;

public class FileStorageService : IFileStorageService
{
    public Stream? GetFileStream(string path)
    {
        if (!File.Exists(path))
            return null;

        return new FileStream(path, FileMode.Open, FileAccess.Read);
    }
}
