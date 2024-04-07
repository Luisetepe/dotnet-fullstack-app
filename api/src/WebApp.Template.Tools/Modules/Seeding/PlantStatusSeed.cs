using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class PlantStatusSeed
{
    public static async Task SeedPlantStatuses(
        WebAppDbContext db,
        IUniqueIdentifierService idService
    )
    {
        // Create 5 new PlantStatus entities
        PlantStatus[] plantStatuses =
        [
            PlantStatus.CreatePlantStatus(idService.Create(), "Operational"),
            PlantStatus.CreatePlantStatus(idService.Create(), "Under construction"),
            PlantStatus.CreatePlantStatus(idService.Create(), "Decommissioned"),
            PlantStatus.CreatePlantStatus(idService.Create(), "Planned"),
            PlantStatus.CreatePlantStatus(idService.Create(), "Closed")
        ];

        // Add the new PlantStatuses to the DbSet
        await db.PlantStatuses.AddRangeAsync(plantStatuses);

        // Save changes to the database
        await db.SaveChangesAsync();
    }
}
