using Application.Auth.LoginCallback;
using Application.Auth.Logout;
using Application.Auth.Refresh;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Presentation.Extensions;

namespace Presentation.Auth;

public class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth");

        group.MapGet("/login", (HttpContext context, IConfiguration config) =>
        {
            var loginEndpoint = config["IdentityProviders:Keycloak:LoginEndpoint"];

            if(string.IsNullOrEmpty(loginEndpoint))
            {
                throw new ArgumentNullException("Login Url can not be null!");
            }

            context.Response.Redirect(loginEndpoint);
            return;
        });

        group.MapGet("/login-callback",
            async (HttpContext context,
                IConfiguration config,
                ISender sender) =>
        {
            var code = context.Request.Query["code"].FirstOrDefault();
            var loginCallbackUrl = config["FrontEnd:LoginCallbackUrl"];
            var unauthenticatedUrl = config["FrontEnd:UnauthenticatedUrl"];

            if (string.IsNullOrEmpty(loginCallbackUrl))
            {
                throw new ArgumentNullException("FrontEnd Url can not be null!");
            }

            if (string.IsNullOrEmpty(unauthenticatedUrl))
            {
                throw new ArgumentNullException("UnauthenticatedUrl can not be null!");
            }

            if (string.IsNullOrEmpty(code))
            {
                context.Response.Redirect(unauthenticatedUrl); // hoặc redirect tới trang logout hoặc trang lỗi
                return;
            }

            var command = new LoginCallbackCommand(code);
            var result = await sender.Send(command);

            if(result.IsFailure)
            {
                context.Response.Redirect(unauthenticatedUrl); // hoặc redirect tới trang logout hoặc trang lỗi
                return;
            }

            var accessToken = result.Value.AccessToken;
            var refreshToken = result.Value.RefreshToken;
            var expiresInUtc = result.Value.ExpiresInUtc;

            // Lưu refresh token vào cookie
            context.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            // Redirect về FE kèm access token qua fragment
            var redirectUrl = $"{loginCallbackUrl}#access_token={accessToken}&expires_in_utc={expiresInUtc}";
            context.Response.Redirect(redirectUrl);
            return;
        });

        group.MapPost("/refresh", async (HttpContext context, ISender sender) =>
        {
            var refreshToken = context.Request.Cookies["refresh_token"];
            var command = new RefreshCommand(refreshToken);
            var result = await sender.Send(command);
            var newRefreshToken = result.Value.RefreshToken;

            context.Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return result.Match(
                onSuccess: value => Results.Ok(value),
                onFailure: handleFailure => handleFailure);
        });

        group.MapPost("/logout", async (HttpContext context, ISender sender) =>
        {
            var refreshToken = context.Request.Cookies["refresh_token"];
            var command = new LogoutCommand(refreshToken);
            var result = await sender.Send(command);
            return result.Match(
                onSuccess: () => Results.NoContent(),
                onFailure: handleFailure => handleFailure);
        });
    }
}
