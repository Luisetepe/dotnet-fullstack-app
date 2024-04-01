using FastEndpoints;
using MediatR;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantById;

public class GetPlantByIdQuery : IRequest<GetPlantByIdResponse>
{
    public GetPlantByIdRequest Request { get; set; }
}

public class GetPlantByIdRequest
{
    public string Id { get; set; }
}

public class GetPlantByIdRequestValidator : Validator<GetPlantByIdRequest>
{
    public GetPlantByIdRequestValidator() { }
}
