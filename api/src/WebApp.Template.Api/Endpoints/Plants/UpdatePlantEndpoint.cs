using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using WebApp.Template.Application.Features.Plants.Commands.UpdatePlant;

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

        await SendResultAsync(response.ToMinimalApiResult());
    }
}

public class UpdatePlantEndpointSwagger : Summary<UpdatePlantEndpoint>
{
    public UpdatePlantEndpointSwagger()
    {
        Summary = "UpdatePlant";
        ExampleRequest = new UpdatePlantRequest();
        Response(
            200,
            "Success" /* example: new() */
        );
        Response(
            500,
            "ERROR" /* example: new("ERROR") */
        );
    }
}
