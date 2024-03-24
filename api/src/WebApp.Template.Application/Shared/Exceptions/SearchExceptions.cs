namespace WebApp.Template.Application.Shared.Exceptions;

public abstract class SearcException : Exception
{
    public SearcException(string message)
        : base(message) { }
}

public class OrderByException : SearcException
{
    public OrderByException(string field)
        : base($"The field '{field}' is not supported for ordering") { }
}
