using Application.Users;
using Domain.Roles;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

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
            x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if(!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var uid = new UserId(parsedUserId);
        //var user = await userRepository.GetUserWithRoles(uid);
        User? user = null;

        if(user is not null && user.DoHavePermission(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
