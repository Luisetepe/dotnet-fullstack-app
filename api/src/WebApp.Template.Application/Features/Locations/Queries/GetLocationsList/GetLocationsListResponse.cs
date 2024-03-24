using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;

public record GetLocationsListResponseDto : BasePaginatedDto
{
    public record Location
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public decimal Longitude { get; init; }
        public decimal Latitude { get; init; }
    }

    public IReadOnlyCollection<Location> Locations { get; init; }
}

public class GetLocationsListResponse : BaseResponse<GetLocationsListResponseDto>
{
    [System.Text.Json.Serialization.JsonConstructor]
    public GetLocationsListResponse(GetLocationsListResponseDto result)
        : base(result) { }

    public GetLocationsListResponse(string message, StatusCode statusCode)
        : base(message, statusCode) { }
}
