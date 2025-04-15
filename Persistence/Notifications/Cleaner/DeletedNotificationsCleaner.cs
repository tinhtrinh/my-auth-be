using Persistence.Cleaners;

namespace Persistence.Notifications.Cleaner;

public class DeletedNotificationsCleaner : IDeletedRecordsCleaner
{
    public Task CleanAsync()
    {
        Console.WriteLine("test clean deleted notifications");
        return Task.CompletedTask;
    }
}
