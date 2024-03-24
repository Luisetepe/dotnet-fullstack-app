using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;

public record GetDashboardWidgetsDataResponseDto
{
    public int Plants { get; init; }
    public int Locations { get; init; }
    public decimal SolarCapacity { get; init; }
    public decimal StorageCapacity { get; init; }
}

public class GetDashboardWidgetsDataResponse : BaseResponse<GetDashboardWidgetsDataResponseDto>
{
    [System.Text.Json.Serialization.JsonConstructor]
    public GetDashboardWidgetsDataResponse(GetDashboardWidgetsDataResponseDto result)
        : base(result) { }

    public GetDashboardWidgetsDataResponse(string message, StatusCode statusCode)
        : base(message, statusCode) { }
}
