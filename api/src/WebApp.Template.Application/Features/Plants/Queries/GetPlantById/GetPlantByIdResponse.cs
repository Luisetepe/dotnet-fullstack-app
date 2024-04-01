using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantById;

public record GetPlantByIdResponseDto
{
    public record Dependency
    {
        public string Id { get; init; }
        public string Name { get; init; }
    }

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
    public string Notes { get; init; }

    public Dependency PlantType { get; init; }
    public Dependency ResourceType { get; init; }
    public Dependency Status { get; init; }
    public Dependency Location { get; init; }
    public IReadOnlyCollection<Dependency> Portfolios { get; init; }
}

public class GetPlantByIdResponse : BaseResponse<GetPlantByIdResponseDto>
{
    [System.Text.Json.Serialization.JsonConstructor]
    public GetPlantByIdResponse(GetPlantByIdResponseDto result)
        : base(result) { }

    public GetPlantByIdResponse(string message, StatusCode statusCode)
        : base(message, statusCode) { }
}
