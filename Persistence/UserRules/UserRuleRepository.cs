using Domain.UserRules;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UserRules;

public class UserRuleRepository : IUserRuleRepository
{
    private readonly MyAuthDbContext _dbContext;

    public UserRuleRepository(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserRule?> GetUserRule() => await _dbContext.Set<UserRule>().FirstOrDefaultAsync();
}