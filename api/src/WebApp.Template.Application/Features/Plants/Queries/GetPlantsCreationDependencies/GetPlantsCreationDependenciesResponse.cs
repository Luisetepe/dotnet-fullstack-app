using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsCreationDependencies;

public record GetPlantsCreationDependenciesDto
{
    public record Dependency
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public IReadOnlyCollection<Dependency> PlantStatuses { get; set; }
    public IReadOnlyCollection<Dependency> Locations { get; set; }
    public IReadOnlyCollection<Dependency> PlantTypes { get; set; }
    public IReadOnlyCollection<Dependency> ResourceTypes { get; set; }
    public IReadOnlyCollection<Dependency> Portfolios { get; set; }
}

public class GetPlantsCreationDependenciesResponse : BaseResponse<GetPlantsCreationDependenciesDto>
{
    [System.Text.Json.Serialization.JsonConstructor]
    public GetPlantsCreationDependenciesResponse(GetPlantsCreationDependenciesDto result)
        : base(result) { }

    public GetPlantsCreationDependenciesResponse(string message, StatusCode statusCode)
        : base(message, statusCode) { }
}
