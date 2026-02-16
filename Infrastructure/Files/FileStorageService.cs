
using Application.Shared.Files;

namespace Infrastructure.Files;

public class FileStorageService : IFileStorageService
{
    private readonly string STORAGE_FOLDER = "D:\\web\\my-auth\\my-auth-be\\Infrastructure\\Files\\Storage\\";

    public Task<Stream?> GetFileStreamAsync(string fileName)
    {
        var path = STORAGE_FOLDER + fileName;
        
        if (!File.Exists(path))
            return Task.FromResult<Stream?>(null);

        return Task.FromResult<Stream?>(new FileStream(path, FileMode.Open, FileAccess.Read));
    }
}
