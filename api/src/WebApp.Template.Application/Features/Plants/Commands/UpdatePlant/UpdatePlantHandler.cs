using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.Services;

namespace WebApp.Template.Application.Features.Plants.Commands.UpdatePlant;

public class UpdatePlantHandler(
    WebAppDbContext dbContext,
    IUniqueIdentifierService identifierService
) : IRequestHandler<UpdatePlantCommand, Result>
{
    public async Task<Result> Handle(
        UpdatePlantCommand command,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var plant = await dbContext
                .Plants.Include(p => p.Portfolios)
                .SingleOrDefaultAsync(
                    p => p.Id == identifierService.ConvertToNumber(command.Request.Id),
                    cancellationToken
                );

            if (plant == null)
                return Result.NotFound($"Plant with ID {command.Request.Id} could not be found.");

            var idToNumber = identifierService.ConvertToNumber;
            var idToString = identifierService.ConvertToString;

            var portfolioIds = command.Request.PortfolioIds.Select(idToNumber).ToArray();
            var portfolios = await dbContext
                .Portfolios.Where(p => portfolioIds.Contains(p.Id))
                .ToArrayAsync(cancellationToken);

            plant.UpdatePlant(
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
                idToNumber(command.Request.LocationId)
            );
            plant.UpdatePortfolios(portfolios);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}
