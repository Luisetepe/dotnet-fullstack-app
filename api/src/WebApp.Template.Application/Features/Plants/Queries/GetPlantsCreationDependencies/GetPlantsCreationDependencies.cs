using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.Services;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsCreationDependencies;

public class GetPlantsCreationDependenciesHandler(
    WebAppDbContext dbContext,
    IUniqueIdentifierService identifierService
) : IRequestHandler<GetPlantsCreationDependenciesQuery, GetPlantsCreationDependenciesResponse>
{
    public async Task<GetPlantsCreationDependenciesResponse> Handle(
        GetPlantsCreationDependenciesQuery query,
        CancellationToken cancellationToken
    )
    {
        var plantStatuses = await dbContext
            .PlantStatuses.Select(x => new GetPlantsCreationDependenciesDto.Dependency
            {
                Id = identifierService.ConvertToString(x.Id),
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        var locations = await dbContext
            .Locations.Select(x => new GetPlantsCreationDependenciesDto.Dependency
            {
                Id = identifierService.ConvertToString(x.Id),
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        var plantTypes = await dbContext
            .PlantTypes.Select(x => new GetPlantsCreationDependenciesDto.Dependency
            {
                Id = identifierService.ConvertToString(x.Id),
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        var resourceTypes = await dbContext
            .ResourceTypes.Select(x => new GetPlantsCreationDependenciesDto.Dependency
            {
                Id = identifierService.ConvertToString(x.Id),
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        var portfolios = await dbContext
            .Portfolios.Select(x => new GetPlantsCreationDependenciesDto.Dependency
            {
                Id = identifierService.ConvertToString(x.Id),
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        var result = new GetPlantsCreationDependenciesDto
        {
            PlantStatuses = plantStatuses,
            Locations = locations,
            PlantTypes = plantTypes,
            ResourceTypes = resourceTypes,
            Portfolios = portfolios
        };

        return new GetPlantsCreationDependenciesResponse(result);
    }
}
