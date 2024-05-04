using Ardalis.Result;
using FastEndpoints;
using FastEndpoints.Security;
using MediatR;
using WebApp.Template.Api.Extensions;
using WebApp.Template.Application.Features.Identity.Commands.SignInUser;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Endpoints.Identity.SignInUser;

public record SignInUserApiResponse
{
    public string Role { get; init; }
    public IReadOnlyCollection<string> AllowedRoutes { get; init; }
    public string Email { get; init; }
}

public class SignInUserEndpoint(ISender mediator)
    : Endpoint<SignInUserRequest, SignInUserApiResponse>
{
    public override void Configure()
    {
        Post("/api/identity/signInUser");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignInUserRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new SignInUserCommand { Request = req }, ct);

        if (!response.IsSuccess)
        {
            await SendResultAsync(response.ToApiResult());
            return;
        }

        await CookieAuth.SignInAsync(u =>
        {
            u.Roles.Add(response.Value.Role);
            u.Claims.Add(new("SessionId", response.Value.SessionId));
            u.Claims.Add(new("UserId", response.Value.UserId));
            u.Claims.Add(new("RoleId", response.Value.RoleId));
        });

        await SendResultAsync(
            response
                .Map(x => new SignInUserApiResponse
                {
                    Role = x.Role,
                    AllowedRoutes = x.AllowedRoutes,
                    Email = x.Email
                })
                .ToApiResult()
        );
    }
}

public class SignInUserEndpointSwagger : Summary<SignInUserEndpoint>
{
    public SignInUserEndpointSwagger()
    {
        Summary = "Signs in a user.";
        Description = "This endpoint is used to sign in a user.";
        ExampleRequest = new SignInUserRequest
        {
            Email = "example@domain.com",
            Password = "password"
        };
        Response(
            200,
            "Success",
            example: new SignInUserApiResponse
            {
                Role = "Admin",
                AllowedRoutes = ["/home/assets/plants/"],
                Email = "example@domain.com",
            }
        );
        Response(401, "The user could not be authenticated.");
        Response(
            400,
            "One or more validation errors occurred while signing in the user.",
            example: ExampleResponses.ExampleValidaitonError(
                new()
                {
                    ["Email"] = ["The 'email' field is required."],
                    ["Password"] = ["The 'password' field is required."]
                }
            )
        );
        Response(
            500,
            "An error occurred while signing in the user.",
            example: ExampleResponses.ExampleCriticalError
        );
    }
}
