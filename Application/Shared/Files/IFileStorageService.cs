namespace Application.Shared.Files;

public interface IFileStorageService
{
    Task<Stream?> GetFileStreamAsync(string fileName);

    Task<string?> UploadFileAsync(string fileName, Stream fileStream);
}
