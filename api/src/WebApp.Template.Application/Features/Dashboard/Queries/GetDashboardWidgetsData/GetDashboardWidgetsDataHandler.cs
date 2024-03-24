using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;

public class GetDashboardWidgetsDataHandler(WebAppDbContext db)
    : IRequestHandler<GetDashboardWidgetsDataQuery, GetDashboardWidgetsDataResponse>
{
    public async Task<GetDashboardWidgetsDataResponse> Handle(
        GetDashboardWidgetsDataQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var locations = await db.Locations.CountAsync(cancellationToken);
            var plants = await db.Plants.CountAsync(cancellationToken);
            var solarCapacity = await db.Plants.SumAsync(p => p.CapacityDc, cancellationToken);
            var storageCapacity = await db.Plants.SumAsync(
                p => p.StorageCapacity,
                cancellationToken
            );

            var response = new GetDashboardWidgetsDataResponse(
                new GetDashboardWidgetsDataResponseDto
                {
                    Locations = locations,
                    Plants = plants,
                    SolarCapacity = solarCapacity,
                    StorageCapacity = storageCapacity
                }
            );

            return response;
        }
        catch (Exception ex)
        {
            return new GetDashboardWidgetsDataResponse(ex.Message, StatusCode.UnhandledError);
        }
    }
}
