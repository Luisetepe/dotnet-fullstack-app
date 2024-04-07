using Ardalis.Result;
using FastEndpoints;
using FluentValidation;
using MediatR;

namespace WebApp.Template.Application.Features.Identity.Commands.SignInUser;

public class SignInUserCommand : IRequest<Result<SignInUserResponse>>
{
    public SignInUserRequest Request { get; set; }
}

public record SignInUserRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public class SignInUserRequestValidator : Validator<SignInUserRequest>
{
    public SignInUserRequestValidator()
    {
        RuleFor(p => p.Email).NotEmpty().EmailAddress();
        RuleFor(p => p.Password).NotEmpty();
    }
}
