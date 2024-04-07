using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;

namespace WebApp.Template.Api.Endpoints.Dashboard;

public class GetDashboardWidgetsDataEndpoint(ISender mediator)
    : EndpointWithoutRequest<GetDashboardWidgetsDataResponse>
{
    public override void Configure()
    {
        Get("/api/dashboard/getDashboardWidgetsData");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await mediator.Send(new GetDashboardWidgetsDataQuery { }, ct);

        await SendResultAsync(response.ToMinimalApiResult());
    }
}

public class GetDashboardWidgetsDataEndpointSwagger : Summary<GetDashboardWidgetsDataEndpoint>
{
    public GetDashboardWidgetsDataEndpointSwagger()
    {
        Summary = "Gets an object representing the data for the dashboard widgets.";
        Response<GetDashboardWidgetsDataResponse>(
            200,
            "An object representing the data for the dashboard widgets."
        // example: new(
        //     new GetDashboardWidgetsDataResponse
        //     {
        //         Locations = 1,
        //         Plants = 2,
        //         SolarCapacity = 100,
        //         StorageCapacity = 200
        //     }
        // )
        );
        Response<GetDashboardWidgetsDataResponse>(
            500,
            "An error occurred while getting the data for the dashboard widgets."
        // example: new(
        //     "An error occurred while getting the data for the dashboard widgets",
        //     StatusCode.UnhandledError
        // )
        );
    }
}
