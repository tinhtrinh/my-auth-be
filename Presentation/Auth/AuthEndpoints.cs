using Application.Auth.LoginCallback;
using Azure;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Json;

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
            var frontendUrl = config["FrontEnd:Url"];
            var unauthenticatedUrl = config["FrontEnd:UnauthenticatedUrl"];

            if (string.IsNullOrEmpty(frontendUrl))
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

            // Lưu token vào cookie
            context.Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true, // bảo mật hơn, JS không đọc được
                Secure = true,   // chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Strict
            });

            context.Response.Cookies.Append("expires_in", expiresInUtc, new CookieOptions
            {
                HttpOnly = true, // bảo mật hơn, JS không đọc được
                Secure = true,   // chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Strict
            });

            context.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            // Redirect về frontend
            context.Response.Redirect(frontendUrl);
            return;
        });
    }
}
