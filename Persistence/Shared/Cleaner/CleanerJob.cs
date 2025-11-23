namespace Persistence.Shared.Cleaner;

public class CleanerJob : ICleanerJob
{
    // nhiều cleaner deleted, inactive, expired... cùng implement icleaner
    // => gom vào 1 cleaner job
    // => 1 backgroundservice chạy
    private readonly ICollection<ICleaner> _cleaners;

    public CleanerJob(ICollection<ICleaner> cleaners)
    {
        _cleaners = cleaners;
    }

    public async Task CleanAsync()
    {
        foreach (var cleaner in _cleaners)
        {
            await cleaner.CleanAsync();
        }
    }
}
