using FastEndpoints;

namespace WebApp.Template.Application.Shared.Models;

public static class ExampleResponses
{
    public static Microsoft.AspNetCore.Mvc.ProblemDetails ExampleCriticalError =>
        new()
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
            Title = "Something went wrong.",
            Status = 500,
            Detail =
                "Next error(s) occurred:* An exception has been raised that is likely due to a transient failure.\n"
        };

    public static ErrorResponse ExampleSearchValidationError =>
        new()
        {
            StatusCode = 400,
            Message = "One or more validation errors occurred!",
            Errors = new()
            {
                ["PageSize"] = ["pageSize must be greater than 0"],
                ["PageNumber"] = ["pageNumber must be greater than 0"],
                ["OrderBy"] = ["order must be 'asc' or 'desc'"]
            }
        };

    public static ErrorResponse ExampleValidaitonError(
        Dictionary<string, List<string>> validationErrors
    )
    {
        return new ErrorResponse
        {
            StatusCode = 400,
            Message = "One or more validation errors occurred!",
            Errors = validationErrors
        };
    }

    public static Microsoft.AspNetCore.Mvc.ProblemDetails ExampleNotFound(string message)
    {
        return new()
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            Title = "Resource not found.",
            Status = 404,
            Detail = $"Next error(s) occurred:* {message}\n"
        };
    }
}
