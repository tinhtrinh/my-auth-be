namespace Application.Shared.Files;

public interface IFileStorageService
{
    Task<Stream?> GetFileStreamAsync(string fileName);

    //Task<FileRecord> UploadFileAsync(IFormFile file);
}
