using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TSID.Creator.NET;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantById;

namespace WebApp.Template.Api.Endpoints.Plants;

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

        await SendResultAsync(response.ToMinimalApiResult());
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
            example: new()
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
                PlantType = new GetPlantByIdResponse.Dependency
                {
                    Id = TsidCreator.GetTsid().ToString(),
                    Name = "PlantType"
                },
                ResourceType = new GetPlantByIdResponse.Dependency
                {
                    Id = TsidCreator.GetTsid().ToString(),
                    Name = "ResourceType"
                },
                Status = new GetPlantByIdResponse.Dependency
                {
                    Id = TsidCreator.GetTsid().ToString(),
                    Name = "Status"
                },
                Location = new GetPlantByIdResponse.Dependency
                {
                    Id = TsidCreator.GetTsid().ToString(),
                    Name = "Location"
                },
                Portfolios =
                [
                    new GetPlantByIdResponse.Dependency
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "Portfolio 1"
                    },
                    new GetPlantByIdResponse.Dependency
                    {
                        Id = TsidCreator.GetTsid().ToString(),
                        Name = "Portfolio 2"
                    }
                ]
            }
        );
        Response<GetPlantByIdResponse>(
            404,
            $"Could not find plant with the provided id."
        // example: new($"Could not find plant with id '{exampleId}'.", StatusCode.NotFoundError)
        );
        Response<GetPlantByIdResponse>(
            500,
            "An error ocurred while getting the plant."
        // example: new("An error ocurred while getting the plant.", StatusCode.UnhandledError)
        );
    }
}
