//using Application.Users.Register;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;
using Carter;
//using Application.Users.Login;
using Domain.Users;
using Application.Users.GetUsers;
//using Infrastructure.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getusers", 
            //[HasPermission(UserPermission.READ_USER)] 
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

        //app.MapPost("/register", async (RegisterRequest request, ISender sender) =>
        //{
        //    var command = RegisterCommand.Create(request);

        //    Result<RegisterResponse> result = await sender.Send(command);

        //    return result.Match(
        //        onSuccess: value => Results.Ok(value),
        //        onFailure: handleFailure => handleFailure);
        //});

        //app.MapPost("/login", async (LoginRequest request, ISender sender) =>
        //{
        //    var command = LoginCommand.Create(request);

        //    Result<LoginResponse> result = await sender.Send(command);

        //    return result.Match(
        //        onSuccess: value => Results.Ok(value),
        //        onFailure: handleFailure => handleFailure);
        //});
    }
}