namespace Persistence.Shared.Cleaner;

public interface ICleanerJob
{
    Task CleanAsync();
}
