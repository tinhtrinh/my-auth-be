namespace Persistence.Cleaners;

public class DeletedRecordsCleanerJob : IDeletedRecordsCleanerJob
{
    private readonly ICollection<IDeletedRecordsCleaner> _cleaners;

    public DeletedRecordsCleanerJob(ICollection<IDeletedRecordsCleaner> cleaners)
    {
        _cleaners = cleaners;
    }

    public async Task CleanAsync()
    {
        foreach(var cleaner in _cleaners)
        {
            await cleaner.CleanAsync();
        }
    }
}
