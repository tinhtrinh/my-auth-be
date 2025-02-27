using Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extensions;

public static class ResultExtensions
{
    public static IResult Match(
        this Result result,
        Func<IResult> onSuccess,
        Func<IResult, IResult> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.HandleFailure());
    }

    public static IResult Match<TValue>(
        this Result<TValue> result,
        Func<TValue, IResult> onSuccess,
        Func<IResult, IResult> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.HandleFailure());
    }

    private static IResult HandleFailure(this Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                Results.BadRequest(
                    CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                Results.BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        default))
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Status = status,
            Detail = error.Message,
            Extensions = { { nameof(errors), errors } }
        };
}