namespace WebApp.Template.Application.Features.Plants.Commands.CreatePlant;

public record CreatePlantResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string PlantId { get; init; }
    public decimal CapacityDc { get; init; }
    public decimal CapacityAc { get; init; }
    public decimal StorageCapacity { get; init; }
    public string ProjectCompany { get; init; }
    public string UtilityCompany { get; init; }
    public int Voltage { get; init; }
    public string AssetManager { get; init; }
    public string Tags { get; init; }
    public string? Notes { get; init; }
    public string PlantTypeId { get; init; }
    public string ResourceTypeId { get; init; }
    public string StatusId { get; init; }
    public string LocationId { get; init; }
}
