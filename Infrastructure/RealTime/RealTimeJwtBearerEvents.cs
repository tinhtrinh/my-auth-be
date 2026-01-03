using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

namespace Infrastructure.RealTime;

public class RealTimeJwtBearerEvents : JwtBearerEvents
{
    private readonly ILogger<RealTimeJwtBearerEvents> _logger;

    public RealTimeJwtBearerEvents(ILogger<RealTimeJwtBearerEvents> logger)
    { 
        _logger = logger; 
    }

    public override Task AuthenticationFailed(AuthenticationFailedContext context) 
    { 
        _logger.LogError(context.Exception, "Real Time Authentication failed"); 
        return Task.CompletedTask; 
    }

    public override Task Challenge(JwtBearerChallengeContext context) 
    { 
        _logger.LogWarning(
            "Real Time Challenge triggered: {Error}, {Description}", 
            context.Error, 
            context.ErrorDescription); 
        return Task.CompletedTask; 
    }

    public override Task MessageReceived(MessageReceivedContext context) 
    { 
        var accessToken = context.Request.Query["access_token"]; 
        var path = context.HttpContext.Request.Path; 
        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/realTimeHub")) 
        { 
            context.Token = accessToken; 
        } 
        return Task.CompletedTask; 
    }
}
