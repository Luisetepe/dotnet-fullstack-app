namespace WebApp.Template.Application.Shared.Exceptions;


public class OrderByException : Exception
{
    public OrderByException(string field) : base($"The field '{field}' is not supported for ordering")
    {
    }
}