using Persistence.Cleaners;

namespace Persistence.Notifications.Cleaner;

public class ReadNotificationsCleaner : IDeletedRecordsCleaner
{
    public Task CleanAsync()
    {
        Console.WriteLine("test read notification cleaner");
        return Task.CompletedTask;
    }
}
