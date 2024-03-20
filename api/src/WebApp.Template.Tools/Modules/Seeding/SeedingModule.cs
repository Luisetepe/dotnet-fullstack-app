using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.Services;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class SeedingModule
{
    public static async Task<ModuleResponse> Run(string connectionString)
    {
        var uuidGenerator = new UniqueIdentifierService();
        
        var options = new DbContextOptionsBuilder<WebAppDbContext>()
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .Options;
        
        await using var db = new WebAppDbContext(options);
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

        return await SeedDatabase(db, uuidGenerator);
    }
    
    public static async Task<ModuleResponse> Run(WebAppDbContext db)
    {
        var uuidGenerator = new UniqueIdentifierService();
        
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

        return await SeedDatabase(db, uuidGenerator);
    }

    private static async Task<ModuleResponse> SeedDatabase(WebAppDbContext db, IUniqueIdentifierService uuidGenerator)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
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

            return new ModuleResponse(false, "Seeding failed. Exception:\n" + e.Message);
        }

        return new ModuleResponse(true, "Seeding complete.");
    }
}