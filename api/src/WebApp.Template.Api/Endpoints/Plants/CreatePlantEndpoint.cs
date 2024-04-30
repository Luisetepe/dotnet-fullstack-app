using FastEndpoints;
using MediatR;
using TSID.Creator.NET;
using WebApp.Template.Api.Extensions;
using WebApp.Template.Application.Features.Plants.Commands.CreatePlant;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Endpoints.Plants.CreatePlant;

public class CreatePlantEndpoint(ISender mediator) : Endpoint<CreatePlantRequest, CreatePlantResponse>
{
    public override void Configure()
    {
        Post("/api/plants/createPlant");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreatePlantRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new CreatePlantCommand { Request = req }, ct);

        await SendResultAsync(response.ToApiResult());
    }
}

public class CreatePlantEndpointSwagger : Summary<CreatePlantEndpoint>
{
    public CreatePlantEndpointSwagger()
    {
        var plantTypeId = TsidCreator.GetTsid().ToString();
        var resourceTypeId = TsidCreator.GetTsid().ToString();
        var statusId = TsidCreator.GetTsid().ToString();
        var locationId = TsidCreator.GetTsid().ToString();

        Summary = "Creates a new plant.";
        Description = "This endpoint is used to create a new plant.";
        ExampleRequest = new CreatePlantRequest
        {
            Name = "Plant Name",
            PlantId = "Plant123",
            CapacityDc = 100.5m,
            CapacityAc = 200.5m,
            StorageCapacity = 300.5m,
            ProjectCompany = "Project Company",
            UtilityCompany = "Utility Company",
            Voltage = 400,
            AssetManager = "Asset Manager",
            Tags = "Tag1, Tag2",
            Notes = "This is a note.",
            PlantTypeId = plantTypeId,
            ResourceTypeId = resourceTypeId,
            StatusId = statusId,
            LocationId = locationId,
            PortfolioIds = [TsidCreator.GetTsid().ToString(), TsidCreator.GetTsid().ToString()]
        };
        Response(
            200,
            "The new plant was created successfully.",
            example: new CreatePlantResponse
            {
                Id = TsidCreator.GetTsid().ToString(),
                Name = "Plant Name",
                PlantId = "Plant123",
                CapacityDc = 100.5m,
                CapacityAc = 200.5m,
                StorageCapacity = 300.5m,
                ProjectCompany = "Project Company",
                UtilityCompany = "Utility Company",
                Voltage = 400,
                AssetManager = "Asset Manager",
                Tags = "Tag1, Tag2",
                Notes = "This is a note.",
                PlantTypeId = plantTypeId,
                ResourceTypeId = resourceTypeId,
                StatusId = statusId,
                LocationId = locationId,
            }
        );
        Response(
            400,
            "One or more validation errors occurred while creating the plant.",
            example: ExampleResponses.ExampleValidaitonError(
                new()
                {
                    ["Name"] = ["Name is required."],
                    ["PlantId"] = ["PlantId is required."],
                    ["CapacityDc"] = ["CapacityDc must be greater than or equal to 0."],
                    ["CapacityAc"] = ["CapacityAc must be greater than or equal to 0."],
                    ["StorageCapacity"] = ["StorageCapacity must be greater than or equal to 0."],
                    ["ProjectCompany"] = ["ProjectCompany is required."],
                    ["UtilityCompany"] = ["UtilityCompany is required."],
                    ["Voltage"] = ["Voltage must be greater than or equal to 0."],
                    ["AssetManager"] = ["AssetManager is required."],
                    ["Tags"] = ["Tags is required."],
                    ["PlantTypeId"] = ["PlantTypeId is required."],
                    ["ResourceTypeId"] = ["ResourceTypeId is required."],
                    ["StatusId"] = ["StatusId is required."],
                    ["LocationId"] = ["LocationId is required."]
                }
            )
        );
        Response(500, "An error occurred while creating the plant.", example: ExampleResponses.ExampleCriticalError);
    }
}
