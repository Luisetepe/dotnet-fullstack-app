using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;

public record GetPlantsListResponseDto : BasePaginatedDto
{
    public record Plant
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string PlantId { get; init; }
        public string UtilityCompany { get; init; }
        public string Status { get; init; }
        public IReadOnlyCollection<string> Tags { get; init; }
        public decimal CapacityDc { get; init; }
        public IReadOnlyCollection<string> Portfolios { get; init; }
    }

    public IReadOnlyCollection<Plant> Plants { get; init; }
}

public class GetPlantsListResponse : BaseResponse<GetPlantsListResponseDto>
{
    [System.Text.Json.Serialization.JsonConstructor]
    public GetPlantsListResponse(GetPlantsListResponseDto result)
        : base(result) { }

    public GetPlantsListResponse(string message) : base(message) { }
}