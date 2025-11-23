using Persistence.Shared.Cleaner;

namespace Persistence.Users.Cleaner;

public class DeletedUsersCleaner : ICleaner
{
    public Task CleanAsync()
    {
        Console.WriteLine("Test clean deleted user, check isdeleted và now đã quá 90 ngày so với lastmodifiedday thì delete");
        return Task.CompletedTask;
    }
}
