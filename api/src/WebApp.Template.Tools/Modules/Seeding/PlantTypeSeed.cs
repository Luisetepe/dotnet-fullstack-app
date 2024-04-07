using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class PlantTypeSeed
{
    public static async Task SeedPlantTypes(WebAppDbContext db, IUniqueIdentifierService idService)
    {
        // Create 5 new PlantType entities
        PlantType[] plantTypes =
        [
            PlantType.CreatePlantType(idService.Create(), "Ground mount"),
            PlantType.CreatePlantType(idService.Create(), "Roof top"),
            PlantType.CreatePlantType(idService.Create(), "Floating"),
            PlantType.CreatePlantType(idService.Create(), "Hybrid"),
            PlantType.CreatePlantType(idService.Create(), "Other")
        ];

        // Add the new PlantTypes to the DbSet
        await db.PlantTypes.AddRangeAsync(plantTypes);

        // Save changes to the database
        await db.SaveChangesAsync();
    }
}
