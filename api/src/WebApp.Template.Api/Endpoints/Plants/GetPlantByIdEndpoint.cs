using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TSID.Creator.NET;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantById;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Api.Endpoints.Plants.GetPlantById;

public class GetPlantByIdEndpoint(ISender mediator)
    : Endpoint<GetPlantByIdRequest, GetPlantByIdResponse>
{
    public override void Configure()
    {
        Get("/api/plants/getPlantById");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        [FromQuery] GetPlantByIdRequest req,
        CancellationToken ct
    )
    {
        var response = await mediator.Send(new GetPlantByIdQuery { Request = req }, ct);

        await SendAsync(response, (int)response.Status, ct);
    }
}

public class GetPlantByIdEndpointSwagger : Summary<GetPlantByIdEndpoint>
{
    public GetPlantByIdEndpointSwagger()
    {
        var exampleId = TsidCreator.GetTsid().ToString();

        Summary = "GetPlantById";
        ExampleRequest = new GetPlantByIdRequest { Id = exampleId };
        Response<GetPlantByIdResponse>(
            200,
            "Success",
            example: new(
                new GetPlantByIdResponseDto
                {
                    Id = exampleId,
                    Name = "PlantName",
                    PlantId = "PlantId",
                    CapacityDc = 1.0M,
                    CapacityAc = 1.0M,
                    StorageCapacity = 1.0M,
                    ProjectCompany = "ProjectCompany",
                    UtilityCompany = "UtilityCompany",
                    Voltage = 1,
                    AssetManager = "AssetManager",
                    Tags = "Tags",
                    Notes = "Notes",
                    PlantType = new GetPlantByIdResponseDto.Dependency
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "PlantType"
                    },
                    ResourceType = new GetPlantByIdResponseDto.Dependency
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "ResourceType"
                    },
                    Status = new GetPlantByIdResponseDto.Dependency
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "Status"
                    },
                    Location = new GetPlantByIdResponseDto.Dependency
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "Location"
                    }
                }
            )
        );
        Response<GetPlantByIdResponse>(
            404,
            $"Could not find plant with the provided id.",
            example: new($"Could not find plant with id '{exampleId}'.", StatusCode.NotFoundError)
        );
        Response<GetPlantByIdResponse>(
            500,
            "An error ocurred while getting the plant.",
            example: new("An error ocurred while getting the plant.", StatusCode.UnhandledError)
        );
    }
}
