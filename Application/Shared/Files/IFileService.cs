namespace Application.Shared.Files;

public interface IFileService
{
    Task<Stream?> GetFileStreamAsync(Guid fileId);

    //Task<FileRecord> UploadFileAsync(IFormFile file);
}
