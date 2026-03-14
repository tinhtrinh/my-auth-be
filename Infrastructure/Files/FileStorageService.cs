
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

    public async Task<string?> UploadFileAsync(string fileName, Stream fileStream)
    {
        if (string.IsNullOrWhiteSpace(fileName) || fileStream == null)
        {
            return null;
        }

        var uploadPath = Path.Combine(STORAGE_FOLDER, fileName);

        using (var file = new FileStream(uploadPath, FileMode.Create, FileAccess.Write))
        {
            await fileStream.CopyToAsync(file);
        }

        return uploadPath;
    }
}
