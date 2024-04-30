using FastEndpoints;
using MediatR;
using TSID.Creator.NET;
using WebApp.Template.Api.Extensions;
using WebApp.Template.Application.Features.Plants.Commands.UpdatePlant;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Endpoints.Plants;

public class UpdatePlantEndpoint(ISender mediator) : Endpoint<UpdatePlantRequest>
{
    public override void Configure()
    {
        Put("/api/plants/updatePlant");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdatePlantRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new UpdatePlantCommand { Request = req }, ct);

        await SendResultAsync(response.ToApiResult());
    }
}

public class UpdatePlantEndpointSwagger : Summary<UpdatePlantEndpoint>
{
    public UpdatePlantEndpointSwagger()
    {
        var plantTypeId = TsidCreator.GetTsid().ToString();
        var resourceTypeId = TsidCreator.GetTsid().ToString();
        var statusId = TsidCreator.GetTsid().ToString();
        var locationId = TsidCreator.GetTsid().ToString();

        Summary = "Updates an existing plant.";
        Description = "This endpoint is used to update an existing plant.";
        ExampleRequest = new UpdatePlantRequest
        {
            Id = TsidCreator.GetTsid().ToString(),
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
        Response(200, "The plant was updated successfully.");
        Response(
            400,
            "One or more validation errors occurred while updating the plant.",
            example: ExampleResponses.ExampleValidaitonError(
                new()
                {
                    ["id"] = ["The 'id' field is required."],
                    ["capacityDc"] = ["The 'capacityDc' field must be greater than or equal to 0."],
                    ["capacityAc"] = ["The 'capacityAc' field must be greater than or equal to 0."],
                    ["storageCapacity"] = ["The 'storageCapacity' field must be greater than or equal to 0."],
                    ["projectCompany"] = ["The 'projectCompany' field is required."],
                    ["utilityCompany"] = ["The 'utilityCompany' field is required."],
                    ["voltage"] = ["The 'voltage' field must be greater than or equal to 0."],
                    ["assetManager"] = ["The 'assetManager' field is required."],
                    ["tags"] = ["The 'tags' field is required."],
                    ["plantTypeId"] = ["The 'plantTypeId' field is required."],
                    ["resourceTypeId"] = ["The 'resourceTypeId' field is required."],
                    ["statusId"] = ["The 'statusId' field is required."],
                    ["locationId"] = ["The 'locationId' field is required."]
                }
            )
        );
        Response(500, "An error occurred while updating the plant.", example: ExampleResponses.ExampleCriticalError);
    }
}
