using Ardalis.Result;
using MediatR;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;

public class GetLocationsListQuery : IRequest<Result<GetLocationsListResponse>>
{
    public GetLocationsListRequest Request { get; set; }
}

public record GetLocationsListRequest : SearchRequest { }

public class GetLocationsListRequestValidator : SearchRequestValidator<GetLocationsListRequest> { }
