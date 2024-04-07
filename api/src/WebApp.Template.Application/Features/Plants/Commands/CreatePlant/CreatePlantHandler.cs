using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Data.Services;

namespace WebApp.Template.Application.Features.Plants.Commands.CreatePlant;

public class CreatePlantHandler(
    WebAppDbContext dbContext,
    IUniqueIdentifierService identifierService
) : IRequestHandler<CreatePlantCommand, Result<CreatePlantResponse>>
{
    public async Task<Result<CreatePlantResponse>> Handle(
        CreatePlantCommand command,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var idToNumber = identifierService.ConvertToNumber;
            var idToString = identifierService.ConvertToString;

            var portfolioIds = command.Request.PortfolioIds.Select(idToNumber).ToArray();
            var portfolios = await dbContext
                .Portfolios.Where(p => portfolioIds.Contains(p.Id))
                .ToArrayAsync(cancellationToken);

            var newPlantResult = Plant.CreatePlant(
                identifierService.Create(),
                command.Request.Name,
                command.Request.PlantId,
                command.Request.CapacityDc,
                command.Request.CapacityAc,
                command.Request.StorageCapacity,
                command.Request.ProjectCompany,
                command.Request.UtilityCompany,
                command.Request.Voltage,
                command.Request.AssetManager,
                command.Request.Tags,
                command.Request.Notes,
                idToNumber(command.Request.PlantTypeId),
                idToNumber(command.Request.ResourceTypeId),
                idToNumber(command.Request.StatusId),
                idToNumber(command.Request.LocationId),
                portfolios
            );

            if (!newPlantResult.IsSuccess)
                return Result.Invalid(newPlantResult.ValidationErrors.ToArray());

            var newPlant = newPlantResult.Value;
            await dbContext.Plants.AddAsync(newPlant, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatePlantResponse
            {
                Id = idToString(newPlant.Id),
                Name = newPlant.Name,
                PlantId = newPlant.PlantId,
                CapacityDc = newPlant.CapacityDc,
                CapacityAc = newPlant.CapacityAc,
                StorageCapacity = newPlant.StorageCapacity,
                ProjectCompany = newPlant.ProjectCompany,
                UtilityCompany = newPlant.UtilityCompany,
                Voltage = newPlant.Voltage,
                AssetManager = newPlant.AssetManager,
                Tags = newPlant.Tags,
                Notes = newPlant.Notes,
                PlantTypeId = idToString(newPlant.PlantTypeId),
                ResourceTypeId = idToString(newPlant.ResourceTypeId),
                StatusId = idToString(newPlant.StatusId),
                LocationId = idToString(newPlant.LocationId),
            };
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}
