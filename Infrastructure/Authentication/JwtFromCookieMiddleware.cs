using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

public class JwtFromCookieMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        const string cookieName = "access_token";
        const string headerName = "Authorization";

        if (context.Request.Cookies.TryGetValue(cookieName, out var token))
        {
            if (!string.IsNullOrEmpty(token) && !context.Request.Headers.ContainsKey(headerName))
            {
                context.Request.Headers[headerName] = $"Bearer {token}";
            }
        }

        await next(context);
    }
}
