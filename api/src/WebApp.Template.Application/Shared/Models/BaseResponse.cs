namespace WebApp.Template.Application.Shared.Models;

public enum StatusCode
{
    Success = 0,
    Error = 1,
}

public class BaseResponse<T> where T : class
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

    public BaseResponse(string message, StatusCode status = StatusCode.Error)
    {
        Status = status;
        Message = message;
        Result = null;
    }
}