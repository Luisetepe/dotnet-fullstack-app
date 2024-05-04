using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Services.Crypto;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class SeedingModule
{
    public static async Task<Result> Run(string connectionString)
    {
        var uuidGenerator = new UniqueIdentifierService();

        var options = new DbContextOptionsBuilder<WebAppDbContext>()
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .Options;

        await using var db = new WebAppDbContext(options);

        return await SeedDatabase(db);
    }

    public static async Task<Result> Run(WebAppDbContext db)
    {
        return await SeedDatabase(db);
    }

    private static async Task<Result> SeedDatabase(WebAppDbContext db)
    {
        var uuidGenerator = new UniqueIdentifierService();
        var cryptoService = new CryptoService();

        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            // Load identity related data
            await IdentitySeed.SeedUsers(db, uuidGenerator, cryptoService);
            // Load first to avoid FK constraint errors
            await LocationSeed.SeedLocations(db, uuidGenerator);
            await PlantTypeSeed.SeedPlantTypes(db, uuidGenerator);
            await PlantStatusSeed.SeedPlantStatuses(db, uuidGenerator);
            await ResourceTypeSeed.SeedResourceTypes(db, uuidGenerator);
            await PortfolioSeed.SeedPortfolios(db, uuidGenerator);

            // Load the plants last
            await PlantSeed.SeedPlants(db, uuidGenerator);

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();

            return Result.CriticalError(
                "Seeding failed. Exception:\n" + e.InnerException?.Message ?? e.Message
            );
        }

        return Result.Success();
    }
}
