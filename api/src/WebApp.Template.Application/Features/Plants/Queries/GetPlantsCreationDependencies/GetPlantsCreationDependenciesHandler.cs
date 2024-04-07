using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsCreationDependencies;

public class GetPlantsCreationDependenciesHandler(WebAppDbContext dbContext)
    : IRequestHandler<
        GetPlantsCreationDependenciesQuery,
        Result<GetPlantsCreationDependenciesResponse>
    >
{
    public async Task<Result<GetPlantsCreationDependenciesResponse>> Handle(
        GetPlantsCreationDependenciesQuery query,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var plantStatuses = await dbContext
                .PlantStatuses.Select(x => new GetPlantsCreationDependenciesResponse.Dependency
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);

            var locations = await dbContext
                .Locations.Select(x => new GetPlantsCreationDependenciesResponse.Dependency
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);

            var plantTypes = await dbContext
                .PlantTypes.Select(x => new GetPlantsCreationDependenciesResponse.Dependency
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);

            var resourceTypes = await dbContext
                .ResourceTypes.Select(x => new GetPlantsCreationDependenciesResponse.Dependency
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);

            var portfolios = await dbContext
                .Portfolios.Select(x => new GetPlantsCreationDependenciesResponse.Dependency
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);

            return new GetPlantsCreationDependenciesResponse
            {
                PlantStatuses = plantStatuses,
                Locations = locations,
                PlantTypes = plantTypes,
                ResourceTypes = resourceTypes,
                Portfolios = portfolios
            };
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}
