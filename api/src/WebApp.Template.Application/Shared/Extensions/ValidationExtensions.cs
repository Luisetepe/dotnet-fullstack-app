using Ardalis.Result;
using FluentValidation;

namespace WebApp.Template.Application.Shared.Extensions;

public static class FluentValidationExtensions
{
    public static List<ValidationError> AsErrors(this ValidationException ex)
    {
        var resultErrors = new List<ValidationError>();

        foreach (var valFailure in ex.Errors)
        {
            resultErrors.Add(
                new ValidationError()
                {
                    Severity = FromSeverity(valFailure.Severity),
                    ErrorMessage = valFailure.ErrorMessage,
                    ErrorCode = valFailure.ErrorCode,
                    Identifier = valFailure.PropertyName
                }
            );
        }

        return resultErrors;
    }

    public static ValidationSeverity FromSeverity(Severity severity)
    {
        switch (severity)
        {
            case Severity.Error:
                return ValidationSeverity.Error;
            case Severity.Warning:
                return ValidationSeverity.Warning;
            case Severity.Info:
                return ValidationSeverity.Info;
            default:
                throw new ArgumentOutOfRangeException(nameof(severity), "Unexpected Severity");
        }
    }
}
