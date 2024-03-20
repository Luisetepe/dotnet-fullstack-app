using FastEndpoints;
using MediatR;
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
        if (response.Status != StatusCode.Success)
        {
            await SendAsync(response, 500, ct);
            return;
        }

        await SendAsync(response, 200, ct);
    }
}

public class GetDashboardWidgetsDataEndpointSwagger : Summary<GetDashboardWidgetsDataEndpoint>
{
    public GetDashboardWidgetsDataEndpointSwagger()
    {
        Summary = "Gets an object representing the data for the dashboard widgets.";
        Response<GetDashboardWidgetsDataResponse>(
            200,
            "An object representing the data for the dashboard widgets.",
            example: new(
                new GetDashboardWidgetsDataResponseDto
                {
                    Locations = 1,
                    Plants = 2,
                    SolarCapacity = 100,
                    StorageCapacity = 200
                }));
        Response<GetDashboardWidgetsDataResponse>(
            500,
            "An error occurred while getting the data for the dashboard widgets.",
            example: new("An error occurred while getting the data for the dashboard widgets")
        );
    }
}