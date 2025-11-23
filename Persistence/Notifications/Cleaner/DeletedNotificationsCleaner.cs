using Persistence.Shared.Cleaner;

namespace Persistence.Notifications.Cleaner;

public class DeletedNotificationsCleaner : ICleaner
{
    public Task CleanAsync()
    {
        Console.WriteLine("Test clean deleted notifications, check isdeleted và now đã quá 90 ngày so với lastmodifiedday thì delete");
        return Task.CompletedTask;
    }
}
