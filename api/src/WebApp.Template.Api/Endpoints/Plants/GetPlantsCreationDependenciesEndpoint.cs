using FastEndpoints;
using MediatR;
using TSID.Creator.NET;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantsCreationDependencies;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Api.Endpoints.Plants.GetPlantsCreationDependenciesEndpoint;

public class GetPlantsCreationDependenciesEndpointEndpoint(ISender mediator)
    : EndpointWithoutRequest<GetPlantsCreationDependenciesResponse>
{
    public override void Configure()
    {
        Get("api/plants/getPlantCreationDependencies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await mediator.Send(new GetPlantsCreationDependenciesQuery(), ct);

        await SendAsync(response, (int)response.Status, ct);
    }
}

public class GetPlantsCreationDependenciesEndpointEndpointSwagger
    : Summary<GetPlantsCreationDependenciesEndpointEndpoint>
{
    public GetPlantsCreationDependenciesEndpointEndpointSwagger()
    {
        Summary = "Gets the dependencies required to create a plant.";
        Response<GetPlantsCreationDependenciesResponse>(
            200,
            "The dependencies required to create a plant.",
            example: new(
                new GetPlantsCreationDependenciesDto
                {
                    PlantTypes =
                    [
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "PlantType1" },
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "PlantType2" }
                    ],
                    PlantStatuses =
                    [
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "PlantStatus1" },
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "PlantStatus2" }
                    ],
                    Locations =
                    [
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "Location1" },
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "Location2" }
                    ],
                    ResourceTypes =
                    [
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "ResourceType1" },
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "ResourceType2" }
                    ],
                    Portfolios =
                    [
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "Portfolio1" },
                        new() { Id = TsidCreator.GetTsid().ToString(), Name = "Portfolio2" }
                    ]
                }
            )
        );
        Response<GetPlantsCreationDependenciesResponse>(
            500,
            "An error ocurred while getting the dependencies required to create a plant.",
            example: new(
                "An error ocurred while getting the dependencies required to create a plant.",
                StatusCode.UnhandledError
            )
        );
    }
}
