using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authorization;

// class này để Dynamic policy, không cần phải khai báo trước tất cả policy trong AddAuthorization, mà sẽ tự động tạo policy với PermissionRequirement
// vừa dùng được dynamic policy, vừa dùng được policy khai báo ở AddAuthorization
public class PermissionAuthorizationPolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options) 
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}