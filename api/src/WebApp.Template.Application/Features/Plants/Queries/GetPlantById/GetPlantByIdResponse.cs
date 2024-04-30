namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantById;

public record GetPlantByIdResponse
{
    public record Dependency
    {
        public string Id { get; init; }
        public string Name { get; init; }
    }

    public string Id { get; init; }
    public string Name { get; init; }
    public string PlantId { get; init; }
    public double CapacityDc { get; init; }
    public double CapacityAc { get; init; }
    public double StorageCapacity { get; init; }
    public string ProjectCompany { get; init; }
    public string UtilityCompany { get; init; }
    public int Voltage { get; init; }
    public string AssetManager { get; init; }
    public string Tags { get; init; }
    public string? Notes { get; init; }

    public Dependency PlantType { get; init; }
    public Dependency ResourceType { get; init; }
    public Dependency Status { get; init; }
    public Dependency Location { get; init; }
    public IReadOnlyCollection<Dependency> Portfolios { get; init; }
}
