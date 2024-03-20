using FastEndpoints;
using MediatR;
using TSID.Creator.NET;
using WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Endpoints.Locations.GetLocationsList;

public class GetLocationsListEndpoint(ISender mediator) : Endpoint<GetLocationsListRequest, GetLocationsListResponse>
{
    public override void Configure()
    {
        Get("/api/locations/getLocationsList");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLocationsListRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new GetLocationsListQuery { Request = req }, ct);

        if (response.Status != StatusCode.Success)
        {
            await SendAsync(response, 500, ct);
            return;
        }

        await SendAsync(response, 200, ct);
    }
}

public class GetLocationsListEndpointSwagger : Summary<GetLocationsListEndpoint>
{
    public GetLocationsListEndpointSwagger()
    {
        Summary = "GetLocationsList";
        ExampleRequest = new GetLocationsListRequest
        {
            PageSize = 10,
            PageNumber = 1,
            Search = "Location 1",
            OrderBy = "name"
        };
        Response<GetLocationsListResponse>(
            200,
            "Success",
            example: new(new GetLocationsListResponseDto
            {
                Locations = new[]
                {
                    new GetLocationsListResponseDto.Location
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "Location 1",
                        Latitude = 1.1m,
                        Longitude = 1.1m
                    },
                    new GetLocationsListResponseDto.Location
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "Location 2",
                        Latitude = 2.2m,
                        Longitude = 2.2m
                    }
                },
                Pagination = new PaginationInfo
                {
                    CurrentPageNumber = 1,
                    CurrentPageSize = 10,
                    TotalRows = 200,
                }
            })
        );
        Response<GetLocationsListResponse>(
            500,
            "ERROR",
            example: new("ERROR"));
    }
}