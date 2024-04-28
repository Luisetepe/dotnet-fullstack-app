using System.Text;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Template.Api.Extensions;

internal static class ResultExtensions
{
    internal static Microsoft.AspNetCore.Http.IResult ToApiResult(this Ardalis.Result.IResult result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => typeof(Result).IsInstanceOfType(result) ? Results.Ok() : Results.Ok(result.GetValue()),
            ResultStatus.NotFound => NotFoundEntity(result),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden => Results.Forbid(),
            ResultStatus.Invalid => ValidationErrorEntity(result),
            ResultStatus.Error => UnprocessableEntity(result),
            ResultStatus.Conflict => ConflictEntity(result),
            ResultStatus.Unavailable => UnavailableEntity(result),
            ResultStatus.CriticalError => CriticalEntity(result),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };
    }

    private static Microsoft.AspNetCore.Http.IResult UnprocessableEntity(Ardalis.Result.IResult result)
    {
        var stringBuilder = new StringBuilder("Next error(s) occurred:");
        foreach (string error in result.Errors)
        {
            stringBuilder.Append("* ").Append(error).AppendLine();
        }

        return Results.UnprocessableEntity(
            new ProblemDetails { Title = "Something went wrong.", Detail = stringBuilder.ToString() }
        );
    }

    private static Microsoft.AspNetCore.Http.IResult NotFoundEntity(Ardalis.Result.IResult result)
    {
        var stringBuilder = new StringBuilder("Next error(s) occurred:");
        if (result.Errors.Any())
        {
            foreach (string error in result.Errors)
            {
                stringBuilder.Append("* ").Append(error).AppendLine();
            }

            return Results.NotFound(
                new ProblemDetails { Title = "Resource not found.", Detail = stringBuilder.ToString() }
            );
        }

        return Results.NotFound();
    }

    private static Microsoft.AspNetCore.Http.IResult ConflictEntity(Ardalis.Result.IResult result)
    {
        var stringBuilder = new StringBuilder("Next error(s) occurred:");
        if (result.Errors.Any())
        {
            foreach (string error in result.Errors)
            {
                stringBuilder.Append("* ").Append(error).AppendLine();
            }

            return Results.Conflict(
                new ProblemDetails { Title = "There was a conflict.", Detail = stringBuilder.ToString() }
            );
        }

        return Results.Conflict();
    }

    private static Microsoft.AspNetCore.Http.IResult CriticalEntity(Ardalis.Result.IResult result)
    {
        var stringBuilder = new StringBuilder("Next error(s) occurred:");
        if (result.Errors.Any())
        {
            foreach (string error in result.Errors)
            {
                stringBuilder.Append("* ").Append(error).AppendLine();
            }

            return Results.Problem(
                new ProblemDetails
                {
                    Title = "Something went wrong.",
                    Detail = stringBuilder.ToString(),
                    Status = 500
                }
            );
        }

        return Results.StatusCode(500);
    }

    private static Microsoft.AspNetCore.Http.IResult UnavailableEntity(Ardalis.Result.IResult result)
    {
        var stringBuilder = new StringBuilder("Next error(s) occurred:");
        if (result.Errors.Any())
        {
            foreach (string error in result.Errors)
            {
                stringBuilder.Append("* ").Append(error).AppendLine();
            }

            return Results.Problem(
                new ProblemDetails
                {
                    Title = "Service unavailable.",
                    Detail = stringBuilder.ToString(),
                    Status = 503
                }
            );
        }

        return Results.StatusCode(503);
    }

    private static Microsoft.AspNetCore.Http.IResult ValidationErrorEntity(Ardalis.Result.IResult result)
    {
        var details = new
        {
            StatusCode = 400,
            Message = "One or more errors occurred!",
            Errors = result
                .ValidationErrors.GroupBy(e => e.Identifier)
                .ToDictionary(
                    group => char.ToLowerInvariant(group.Key[0]) + group.Key[1..],
                    group => group.Select(e => e.ErrorMessage).ToArray()
                )
        };

        return Results.BadRequest(details);
    }
}
