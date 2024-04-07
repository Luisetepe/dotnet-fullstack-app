namespace WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;

public record GetDashboardWidgetsDataResponse
{
    public int Plants { get; init; }
    public int Locations { get; init; }
    public decimal SolarCapacity { get; init; }
    public decimal StorageCapacity { get; init; }
}
