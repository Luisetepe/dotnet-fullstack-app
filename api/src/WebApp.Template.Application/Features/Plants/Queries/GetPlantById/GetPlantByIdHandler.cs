using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.Services;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantById;

public class GetPlantByIdHandler(
    WebAppDbContext dbContext,
    IUniqueIdentifierService identifierService
) : IRequestHandler<GetPlantByIdQuery, GetPlantByIdResponse>
{
    public async Task<GetPlantByIdResponse> Handle(
        GetPlantByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        var id = identifierService.ConvertToNumber(query.Request.Id);

        var plant = await dbContext
            .Plants.Include(p => p.PlantType)
            .Include(p => p.ResourceType)
            .Include(p => p.Status)
            .Include(p => p.Location)
            .Include(p => p.Portfolios)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (plant is null)
        {
            return new GetPlantByIdResponse(
                $"Could not find plant with id '{query.Request.Id}'.",
                StatusCode.NotFoundError
            );
        }

        return new GetPlantByIdResponse(
            new GetPlantByIdResponseDto
            {
                Id = identifierService.ConvertToString(plant.Id),
                Name = plant.Name,
                PlantId = plant.PlantId,
                CapacityDc = plant.CapacityDc,
                CapacityAc = plant.CapacityAc,
                StorageCapacity = plant.StorageCapacity,
                ProjectCompany = plant.ProjectCompany,
                UtilityCompany = plant.UtilityCompany,
                Voltage = plant.Voltage,
                AssetManager = plant.AssetManager,
                Tags = plant.Tags,
                Notes = plant.Notes,
                PlantType = new GetPlantByIdResponseDto.Dependency
                {
                    Id = identifierService.ConvertToString(plant.PlantType.Id),
                    Name = plant.PlantType.Name
                },
                ResourceType = new GetPlantByIdResponseDto.Dependency
                {
                    Id = identifierService.ConvertToString(plant.ResourceType.Id),
                    Name = plant.ResourceType.Name
                },
                Status = new GetPlantByIdResponseDto.Dependency
                {
                    Id = identifierService.ConvertToString(plant.Status.Id),
                    Name = plant.Status.Name
                },
                Location = new GetPlantByIdResponseDto.Dependency
                {
                    Id = identifierService.ConvertToString(plant.Location.Id),
                    Name = plant.Location.Name
                },
                Portfolios = plant
                    .Portfolios.Select(portfolio => new GetPlantByIdResponseDto.Dependency
                    {
                        Id = identifierService.ConvertToString(portfolio.Id),
                        Name = portfolio.Name
                    })
                    .ToArray()
            }
        );
    }
}
