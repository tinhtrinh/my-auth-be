using Application.Users.Delete;
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
    }
}
