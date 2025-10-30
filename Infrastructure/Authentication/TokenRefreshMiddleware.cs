using Application.Shared.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

public class TokenRefreshMiddleware : IMiddleware
{
    private readonly IJwtProvider _jwtProvider;

    public TokenRefreshMiddleware(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Kiểm tra endpoint hiện tại
        var endpoint = context.GetEndpoint();
        var hasAuthorize = endpoint?.Metadata?.GetMetadata<IAuthorizeData>() != null;

        if (hasAuthorize)
        {
            var accessToken = context.Request.Cookies["access_token"];
            var refreshToken = context.Request.Cookies["refresh_token"];

            if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
            {
                var expiresIn = context.Request.Cookies["expires_in"];

                if(!string.IsNullOrEmpty(expiresIn) && DateTime.TryParse(expiresIn, out DateTime expireTime))
                {
                    var isExpired = DateTime.UtcNow > expireTime;

                    if(isExpired)
                    {
                        var newToken = await _jwtProvider.RefreshTokenAsync(refreshToken);
                        if (newToken != null)
                        {
                            var newAccessToken = newToken.AccessToken;
                            var newRefreshToken = newToken.RefreshToken;
                            var newExpiresIn = newToken.ExpiresIn;

                            if(!string.IsNullOrEmpty(newAccessToken) 
                                && !string.IsNullOrEmpty(newRefreshToken)
                                && newExpiresIn > 0)
                            {
                                var expiresInUtc = DateTime.UtcNow.AddSeconds(newToken.ExpiresIn).ToString();

                                context.Response.Cookies.Append("access_token", newAccessToken, new CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    SameSite = SameSiteMode.Strict
                                });

                                context.Response.Cookies.Append("expires_in", expiresInUtc, new CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    SameSite = SameSiteMode.Strict
                                });

                                context.Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    SameSite = SameSiteMode.Strict
                                });
                            }
                        }
                    }
                }
            }
        }

        await next(context);
    }
}
