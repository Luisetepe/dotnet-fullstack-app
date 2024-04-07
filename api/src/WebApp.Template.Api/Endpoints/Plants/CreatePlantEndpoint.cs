using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using TSID.Creator.NET;
using WebApp.Template.Application.Features.Plants.Commands.CreatePlant;

namespace WebApp.Template.Endpoints.Plants.CreatePlant;

public class CreatePlantEndpoint(ISender mediator)
    : Endpoint<CreatePlantRequest, CreatePlantResponse>
{
    public override void Configure()
    {
        Post("/api/plants/createPlant");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreatePlantRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new CreatePlantCommand { Request = req }, ct);

        await SendResultAsync(response.ToMinimalApiResult());
    }
}

public class CreatePlantEndpointSwagger : Summary<CreatePlantEndpoint>
{
    public CreatePlantEndpointSwagger()
    {
        Summary = "Creates a new plant.";
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
            PlantTypeId = TsidCreator.GetTsid().ToString(),
            ResourceTypeId = TsidCreator.GetTsid().ToString(),
            StatusId = TsidCreator.GetTsid().ToString(),
            LocationId = TsidCreator.GetTsid().ToString(),
            PortfolioIds = [TsidCreator.GetTsid().ToString(), TsidCreator.GetTsid().ToString()]
        };
        Response<CreatePlantResponse>(200, "Success", example: new());
        Response<CreatePlantResponse>(500, "ERROR", example: new());
    }
}
