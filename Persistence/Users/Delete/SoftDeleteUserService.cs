using Application.Users.Delete;
using Domain.AuditLogs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Users.Delete;

public class SoftDeleteUserService : ISoftDeleteUserService
{
    private readonly MyAuthDbContext _dbContext;

    public SoftDeleteUserService(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteAsync(string id)
    {
        var userId = new SqlParameter("@UserId", id);
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC usp_SoftDeleteUser @UserId", userId);

        var auditLog = new AuditLog.Builder(
            new AuditLogId(Guid.NewGuid()),
            "Deleted",
            "User",
            new Guid(id),
            new Domain.Users.UserId(new Guid("E3CFF717-B833-49BD-8B0E-3919BE992B7C")),
            DateTime.UtcNow)
        .SetIsDeleted(false)
        .Build();

        _dbContext.Set<AuditLog>().Add(auditLog);
        await _dbContext.SaveChangesAsync();
    }
}
