using Persistence.Cleaners;

namespace Persistence.Users.Cleaner;

public class DeletedUsersCleaner : IDeletedRecordsCleaner
{
    public Task CleanAsync()
    {
        Console.WriteLine("test clean deleted user");
        return Task.CompletedTask;
    }
}
