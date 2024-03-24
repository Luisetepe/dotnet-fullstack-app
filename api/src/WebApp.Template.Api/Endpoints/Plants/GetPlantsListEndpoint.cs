using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TSID.Creator.NET;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Api.Endpoints.Plants;

public class GetPlantsListEndpoint(ISender mediator)
    : Endpoint<GetPlantsListRequest, GetPlantsListResponse>
{
    public override void Configure()
    {
        Get("/api/plants/getPlantsList");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        [FromQuery] GetPlantsListRequest req,
        CancellationToken ct
    )
    {
        var response = await mediator.Send(new GetPlantsListQuery { Request = req }, ct);

        await SendAsync(response, (int)response.Status, ct);
    }
}

public class GetPlantsListEndpointSwagger : Summary<GetPlantsListEndpoint>
{
    public GetPlantsListEndpointSwagger()
    {
        Summary = "Gets a list of plants";
        ExampleRequest = new GetPlantsListRequest
        {
            PageSize = 10,
            PageNumber = 1,
            Search = "Plant 1",
            OrderBy = "name",
        };
        Response<GetPlantsListResponse>(
            200,
            "A list of the Plants in the system",
            example: new(
                new GetPlantsListResponseDto
                {
                    Plants = new[]
                    {
                        new GetPlantsListResponseDto.Plant
                        {
                            Id = TsidCreator.GetTsid().ToString(),
                            Name = "Plant 1",
                            PlantId = "PL01",
                            UtilityCompany = "Utility Company 1",
                            Status = "Active",
                            Tags = ["Tag 1", "Tag 2"],
                            CapacityDc = 100,
                            Portfolios = ["Portfolio 1", "Portfolio 2"]
                        },
                        new GetPlantsListResponseDto.Plant
                        {
                            Id = TsidCreator.GetTsid().ToString(),
                            Name = "Plant 2",
                            PlantId = "PL02",
                            UtilityCompany = "Utility Company 2",
                            Status = "Decommissioned",
                            Tags = ["Tag 3", "Tag 4"],
                            CapacityDc = 200,
                            Portfolios = ["Portfolio 3", "Portfolio 4"]
                        },
                    },
                    Pagination = new PaginationInfo
                    {
                        CurrentPageNumber = 1,
                        CurrentPageSize = 10,
                        TotalRows = 200,
                    }
                }
            )
        );
        Response<GetPlantsListResponse>(
            500,
            "An error occurred while getting the list of Plants",
            example: new(
                "An error occurred while getting the list of Plants",
                StatusCode.UnhandledError
            )
        );
    }
}
