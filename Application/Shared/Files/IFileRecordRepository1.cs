using Domain.Files;
using System;

namespace Application.Shared.Files;

public interface IFileRecordRepository1
{
    Task<FileRecord?> GetByIdAsync(Guid id);

    Task<IEnumerable<FileRecord>> GetAllAsync();

    Task AddAsync(FileRecord fileRecord);

    Task UpdateAsync(FileRecord fileRecord);

    Task DeleteAsync(int id);
}

//public class FileRecordRepository : IFileRecordRepository
//{
//    private readonly AppDbContext _dbContext;

//    public FileRecordRepository(AppDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public async Task<FileRecord?> GetByIdAsync(int id)
//    {
//        return await _dbContext.Files.FindAsync(id);
//    }

//    public async Task<IEnumerable<FileRecord>> GetAllAsync()
//    {
//        return await _dbContext.Files.ToListAsync();
//    }

//    public async Task AddAsync(FileRecord fileRecord)
//    {
//        _dbContext.Files.Add(fileRecord);
//        await _dbContext.SaveChangesAsync();
//    }

//    public async Task UpdateAsync(FileRecord fileRecord)
//    {
//        _dbContext.Files.Update(fileRecord);
//        await _dbContext.SaveChangesAsync();
//    }

//    public async Task DeleteAsync(int id)
//    {
//        var record = await _dbContext.Files.FindAsync(id);
//        if (record != null)
//        {
//            _dbContext.Files.Remove(record);
//            await _dbContext.SaveChangesAsync();
//        }
//    }
//}

//public interface IFileStorageService
//{
//    Task<string> SaveFileAsync(IFormFile file);
//    Task<Stream> GetFileAsync(string path);
//    Task DeleteFileAsync(string path);
//}

//public class LocalFileStorageService : IFileStorageService
//{
//    private readonly string _rootPath = Path.Combine("wwwroot", "uploads");

//    public async Task<string> SaveFileAsync(IFormFile file)
//    {
//        if (!Directory.Exists(_rootPath))
//            Directory.CreateDirectory(_rootPath);

//        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
//        var fullPath = Path.Combine(_rootPath, fileName);

//        using (var stream = new FileStream(fullPath, FileMode.Create))
//        {
//            await file.CopyToAsync(stream);
//        }

//        return fullPath; // trả về path để lưu vào DB
//    }

//    public async Task<Stream> GetFileAsync(string path)
//    {
//        if (!File.Exists(path))
//            throw new FileNotFoundException();

//        return new FileStream(path, FileMode.Open, FileAccess.Read);
//    }

//    public Task DeleteFileAsync(string path)
//    {
//        if (File.Exists(path))
//            File.Delete(path);

//        return Task.CompletedTask;
//    }
//}

//public class FileAppService
//{
//    private readonly IFileRecordRepository _fileRecordRepo;
//    private readonly IFileStorageService _fileStorage;

//    public FileAppService(IFileRecordRepository fileRecordRepo, IFileStorageService fileStorage)
//    {
//        _fileRecordRepo = fileRecordRepo;
//        _fileStorage = fileStorage;
//    }

//    public async Task<FileRecord> UploadFileAsync(IFormFile file)
//    {
//        var path = await _fileStorage.SaveFileAsync(file);

//        var record = new FileRecord
//        {
//            FileName = file.FileName,
//            Path = path,
//            Size = file.Length,
//            CreatedDate = DateTime.UtcNow,
//            ContentType = file.ContentType
//        };

//        await _fileRecordRepo.AddAsync(record);
//        return record;
//    }

//    public async Task<Stream?> GetFileStreamAsync(Guid id)
//    {
//        var record = await _fileRecordRepo.GetByIdAsync(id);
//        if (record == null)
//            return null;

//        return await _fileStorage.GetFileAsync(record.Path);
//    }
//}