using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using WebApp.Template.Application.Features.Identity.Queries.CheckAllowedRoute;

namespace WebApp.Template.Endpoints.Identity.CheckAllowedRoute;

public class CheckAllowedRouteEndpoint(ISender mediator) : Endpoint<CheckAllowedRouteRequest>
{
    public override void Configure()
    {
        Get("/api/identity/checkAllowedRoute");
    }

    public override async Task HandleAsync(CheckAllowedRouteRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(
            new CheckAllowedRouteQuery { Request = req, User = User },
            ct
        );

        await SendResultAsync(response.ToMinimalApiResult());
    }
}

public class CheckAllowedRouteEndpointSwagger : Summary<CheckAllowedRouteEndpoint>
{
    public CheckAllowedRouteEndpointSwagger()
    {
        Summary = "CheckAllowedRoute";
        ExampleRequest = new CheckAllowedRouteRequest();
        // Response(200, "Success", example: new());
        // Response(500, "ERROR", example: new("ERROR"));
    }
}
