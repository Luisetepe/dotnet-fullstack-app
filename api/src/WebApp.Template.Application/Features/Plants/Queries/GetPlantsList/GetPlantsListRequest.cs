using MediatR;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;

public class GetPlantsListQuery : IRequest<GetPlantsListResponse>
{
    public GetPlantsListRequest Request { get; set; }
}

public record GetPlantsListRequest : SearchRequest { }

public class GetPlantsListRequestValidator : SearchRequestValidator<GetPlantsListRequest> { }
