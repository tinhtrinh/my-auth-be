namespace Infrastructure.Files;

public interface IFileStorageService
{
    Stream? GetFileStream(string path);
}
