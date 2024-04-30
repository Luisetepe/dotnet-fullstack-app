namespace WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;

public record GetDashboardWidgetsDataResponse
{
    public int Plants { get; init; }
    public int Locations { get; init; }
    public double SolarCapacity { get; init; }
    public double StorageCapacity { get; init; }
}
