using Ardalis.Result.AspNetCore;
using FastEndpoints;
using FastEndpoints.Security;
using MediatR;
using WebApp.Template.Application.Features.Identity.Commands.SignInUser;

namespace WebApp.Template.Endpoints.Identity.SignInUser;

public class SignInUserEndpoint(ISender mediator) : Endpoint<SignInUserRequest, SignInUserResponse>
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
            await SendResultAsync(response.ToMinimalApiResult());
            return;
        }

        await CookieAuth.SignInAsync(u =>
        {
            u.Roles.Add(response.Value.Role);
            u.Claims.Add(new("SessionId", response.Value.SessionId));
            u.Claims.Add(new("UserId", response.Value.UserId));
            u.Claims.Add(new("RoleId", response.Value.RoleId));
        });

        await SendResultAsync(response.ToMinimalApiResult());
    }
}

public class SignInUserEndpointSwagger : Summary<SignInUserEndpoint>
{
    public SignInUserEndpointSwagger()
    {
        Summary = "SignInUser";
        ExampleRequest = new SignInUserRequest();
        Response<SignInUserResponse>(200, "Success", example: new());
        Response<SignInUserResponse>(500, "ERROR", example: new());
    }
}
