using FastEndpoints;
using MediatR;
using WebApp.Template.Api.Extensions;
using WebApp.Template.Application.Features.Identity.Queries.CheckAllowedRoute;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Endpoints.Identity.CheckAllowedRoute;

public class CheckAllowedRouteEndpoint(ISender mediator) : Endpoint<CheckAllowedRouteRequest>
{
    public override void Configure()
    {
        Get("/api/identity/checkAllowedRoute");
    }

    public override async Task HandleAsync(CheckAllowedRouteRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new CheckAllowedRouteQuery { Request = req, User = User }, ct);

        await SendResultAsync(response.ToApiResult());
    }
}

public class CheckAllowedRouteEndpointSwagger : Summary<CheckAllowedRouteEndpoint>
{
    public CheckAllowedRouteEndpointSwagger()
    {
        Summary = "Checks if the user is allowed to access a route.";
        ExampleRequest = new CheckAllowedRouteRequest { Route = "/home/assets/plants/" };
        Response(200, "The user is allowed to access the route.");
        Response(403, "The user is not allowed to access the route.");
        Response(
            400,
            "The route is not valid.",
            example: ExampleResponses.ExampleValidaitonError(new() { ["Route"] = ["The 'route' field is required."] })
        );
        Response(
            500,
            "An error occurred while checking if the user is allowed to access the route.",
            example: ExampleResponses.ExampleCriticalError
        );
    }
}
