namespace Persistence.Cleaners;

public interface IDeletedRecordsCleanerJob
{
    Task CleanAsync();
}
