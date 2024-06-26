using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Application.Features.Plants.Commands.UpdatePlant;

public class UpdatePlantHandler(WebAppDbContext dbContext, IUniqueIdentifierService idService)
    : IRequestHandler<UpdatePlantCommand, Result>
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
                .SingleOrDefaultAsync(p => p.Id == command.Request.Id, cancellationToken);

            if (plant == null)
            {
                return Result.NotFound($"Plant with ID {command.Request.Id} could not be found.");
            }

            var idToNumber = idService.ConvertToNumber;
            var idToString = idService.ConvertToString;

            var portfolios = await dbContext
                .Portfolios.Where(p => command.Request.PortfolioIds.Contains(p.Id))
                .ToArrayAsync(cancellationToken);

            var updateResult = plant.UpdatePlant(
                command.Request.CapacityDc,
                command.Request.CapacityAc,
                command.Request.StorageCapacity,
                command.Request.ProjectCompany,
                command.Request.UtilityCompany,
                command.Request.Voltage,
                command.Request.AssetManager,
                command.Request.Tags,
                command.Request.Notes,
                command.Request.PlantTypeId,
                command.Request.ResourceTypeId,
                command.Request.StatusId,
                command.Request.LocationId
            );

            if (!updateResult.IsSuccess)
            {
                return Result.Invalid(updateResult.ValidationErrors.ToArray());
            }

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
