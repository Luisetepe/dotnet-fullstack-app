using FastEndpoints;
using MediatR;
using WebApp.Template.Api.Extensions;
using WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Api.Endpoints.Dashboard;

public class GetDashboardWidgetsDataEndpoint(ISender mediator) : EndpointWithoutRequest<GetDashboardWidgetsDataResponse>
{
    public override void Configure()
    {
        Get("/api/dashboard/getDashboardWidgetsData");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await mediator.Send(new GetDashboardWidgetsDataQuery { }, ct);

        await SendResultAsync(response.ToApiResult());
    }
}

public class GetDashboardWidgetsDataEndpointSwagger : Summary<GetDashboardWidgetsDataEndpoint>
{
    public GetDashboardWidgetsDataEndpointSwagger()
    {
        Summary = "Gets an object representing the data for the dashboard widgets.";
        Description = "This endpoint is used to get the data for the dashboard widgets.";
        Response(
            200,
            "An object representing the data for the dashboard widgets.",
            example: new GetDashboardWidgetsDataResponse
            {
                Locations = 1,
                Plants = 2,
                SolarCapacity = 100,
                StorageCapacity = 200
            }
        );
        Response(
            500,
            "An error occurred while getting the data for the dashboard widgets.",
            example: ExampleResponses.ExampleCriticalError
        );
    }
}
