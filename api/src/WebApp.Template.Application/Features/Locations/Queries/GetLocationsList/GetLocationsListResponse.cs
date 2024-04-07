using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;

public record GetLocationsListResponse : BasePaginatedResponse
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
