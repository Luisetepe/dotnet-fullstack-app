namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsCreationDependencies;

public record GetPlantsCreationDependenciesResponse
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
