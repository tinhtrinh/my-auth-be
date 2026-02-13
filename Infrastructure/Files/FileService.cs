using Application.Shared.Files;
using Domain.Files;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Files;

public class FileService : IFileService
{
    private readonly MyAuthDbContext _dbContext;
    private readonly IFileStorageService _fileStorage;

    public FileService(MyAuthDbContext dbContext, IFileStorageService fileStorage)
    {
        _dbContext = dbContext;
        _fileStorage = fileStorage;
    }

    public async Task<Stream?> GetFileStreamAsync(Guid fileId)
    {
        var fileRecord = await _dbContext.Set<FileRecord>()
            .AsNoTracking()
            .FirstOrDefaultAsync(fr => fr.Id == fileId);

        if (fileRecord is null || string.IsNullOrEmpty(fileRecord.Path))
        {
            return null;
        }

        var stream = _fileStorage.GetFileStream(fileRecord.Path);

        return stream;
    }
}
