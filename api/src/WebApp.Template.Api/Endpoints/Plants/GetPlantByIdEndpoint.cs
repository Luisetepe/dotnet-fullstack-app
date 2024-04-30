using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TSID.Creator.NET;
using WebApp.Template.Api.Extensions;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantById;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Api.Endpoints.Plants;

public class GetPlantByIdEndpoint(ISender mediator) : Endpoint<GetPlantByIdRequest, GetPlantByIdResponse>
{
    public override void Configure()
    {
        Get("/api/plants/getPlantById");
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] GetPlantByIdRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new GetPlantByIdQuery { Request = req }, ct);

        await SendResultAsync(response.ToApiResult());
    }
}

public class GetPlantByIdEndpointSwagger : Summary<GetPlantByIdEndpoint>
{
    public GetPlantByIdEndpointSwagger()
    {
        var exampleId = TsidCreator.GetTsid().ToString();

        Summary = "Gets a plant by id.";
        Description = "This endpoint is used to get a plant by id.";
        ExampleRequest = new GetPlantByIdRequest { Id = exampleId };
        Response(
            200,
            "The plant was retrieved successfully.",
            example: new GetPlantByIdResponse
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
                PlantType = new() { Id = TsidCreator.GetTsid().ToString(), Name = "PlantType" },
                ResourceType = new() { Id = TsidCreator.GetTsid().ToString(), Name = "ResourceType" },
                Status = new() { Id = TsidCreator.GetTsid().ToString(), Name = "Status" },
                Location = new() { Id = TsidCreator.GetTsid().ToString(), Name = "Location" },
                Portfolios =
                [
                    new() { Id = TsidCreator.GetTsid().ToString(), Name = "Portfolio 1" },
                    new() { Id = TsidCreator.GetTsid().ToString(), Name = "Portfolio 2" }
                ]
            }
        );
        Response(
            400,
            "One or more errors occurred while getting the plant.",
            example: ExampleResponses.ExampleValidaitonError(new() { ["Id"] = ["Id must not be empty"] })
        );
        Response(
            404,
            $"Could not find plant with the provided id.",
            example: ExampleResponses.ExampleNotFound($"Plant with ID {exampleId} could not be found.")
        );
        Response(500, "An error ocurred while getting the plant.", example: ExampleResponses.ExampleCriticalError);
    }
}
