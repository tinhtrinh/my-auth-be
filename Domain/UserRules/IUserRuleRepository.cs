namespace Domain.UserRules;

public interface IUserRuleRepository
{
    Task<UserRule?> GetUserRule();
}