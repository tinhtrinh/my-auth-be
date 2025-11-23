using Persistence.Shared.Cleaner;

namespace Persistence.Notifications.Cleaner;

public class ReadNotificationsCleaner : ICleaner
{
    public Task CleanAsync()
    {
        Console.WriteLine("Test clean read notification, check isread và now đã quá 90 ngày so với lastmodifiedday thì delete");
        return Task.CompletedTask;
    }
}
