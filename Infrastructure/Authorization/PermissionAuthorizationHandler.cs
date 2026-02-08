using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Infrastructure.Authorization;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if(!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        // phải lấy userRepository kiểu này để khi hàm chạy xong thì hủy vòng đời luôn, không gây tốn tài nguyên,
        // không bị phụ thuộc vào vòng đời của class cha là singleton, mà đúng với cách nó đăng ký là scope
        IAuthorizationUserService authorizationUserRepository = scope.ServiceProvider.GetRequiredService<IAuthorizationUserService>();

        var uid = new UserId(parsedUserId);
        var user = await authorizationUserRepository.GetUserWithRoles(uid);

        if(user is null)
        {
            var reason = new AuthorizationFailureReason(this, UserError.UserNotFound.Message);
            context.Fail(reason);
            return;
        }

        if(user is not null && user.HasNoRole())
        {
            var reason = new AuthorizationFailureReason(this, UserError.NoRole.Message);
            context.Fail(reason);
            return;
        }

        if (user is not null && !user.HasPermission(requirement.Permission))
        {
            var reason = new AuthorizationFailureReason(this, UserError.NoRequirePermission.Message);
            context.Fail(reason);
            return;
        }

        if (user is not null && user.HasPermission(requirement.Permission))
        {
            context.Succeed(requirement);
            return;
        }
    }
}
