namespace WebApp.Template.Application.Shared.Models;

public enum StatusCode
{
    Success = 200,
    UnhandledError = 500,
    ValidationError = 400,
    NotFoundError = 404,
    UnauthorizedError = 401
}

public class BaseResponse<T>
    where T : class
{
    public StatusCode Status { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }

    public BaseResponse(T result)
    {
        Status = StatusCode.Success;
        Result = result;
        Message = null;
    }

    public BaseResponse(string message, StatusCode status = StatusCode.UnhandledError)
    {
        Status = status;
        Message = message;
        Result = null;
    }
}
