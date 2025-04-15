namespace Persistence.Cleaners;

public interface IDeletedRecordsCleaner
{
    Task CleanAsync();
}
