//using Application.Users.Register;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;
using Carter;
//using Application.Users.Login;
using Application.Users.GetUsers;
using Application.Users.Register;
using Application.Users.Delete;
using Application.Users.Update;
using Application.Users.ListView;
using Application.Users.SendVerifyEmail;
using Infrastructure.Authorization;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Application.Users.Login;
using Application.Users.Export;

namespace Presentation.User;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users");

        group.MapGet("", 
            [HasPermission(UserPermission.READ_USER)]
        async (
            [AsParameters] GetUsersRequest request,
            ISender sender) =>
        {
            var query = new GetUsersQuery(request);

            Result<GetUsersResponse> result = await sender.Send(query);

            return result.Match(
                onSuccess: value => Results.Ok(value),
                onFailure: handleFailure => handleFailure);
        });

        group.MapPost("/register", async (RegisterRequest request, ISender sender) =>
        {
            var command = new RegisterCommand(request);

            Result<RegisterResponse> result = await sender.Send(command);

            return result.Match(
                onSuccess: value => Results.Ok(value),
                onFailure: handleFailure => handleFailure);
        });

        group.MapDelete("", async (string id, ISender sender) =>
        {
            var command = new DeleteCommand(id);

            Result result = await sender.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: handleFailure => handleFailure);
        });

        group.MapPatch("", async (UpdateRequest request, ISender sender) =>
        {
            var command = new UpdateCommand(request);

            var result = await sender.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: handleFailure => handleFailure);
        });

        group.MapPost("/list-view", async (GetUserListRequest request, ISender sender) =>
        {
            Result<GetUserListResponse> result = await sender.Send(request);

            return result.Match(
                onSuccess: value => Results.Ok(value),
                onFailure: handleFailure => handleFailure);
        });

        group.MapPost("/send-verify-email", async (ISender sender) =>
        {
            var command = new SendVerifyEmailCommand();
            Result result = await sender.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: handleFailure => handleFailure);
        });

        group.MapGet("/export",
        async (
            string connectionId,
            HttpContext httpContext,
            ISender sender) =>
        {
            var authHeader = httpContext.Request.Headers["Authorization"].ToString();

            string? token = null; 
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ")) 
            { 
                token = authHeader.Substring("Bearer ".Length).Trim(); 
            }

            var query = new ExportQuery(connectionId, token);

            Result result = await sender.Send(query);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: handleFailure => handleFailure);
        });

        //app.MapPost("/login", async (ISender sender) =>
        //{
        //    var command = new LoginCommand();

        //    Result<LoginResponse> result = await sender.Send(command);

        //    return result.Match(
        //        onSuccess: value => Results.Ok(value),
        //        onFailure: handleFailure => handleFailure);
        //});
    }
}