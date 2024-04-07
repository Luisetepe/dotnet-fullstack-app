using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class ResourceTypeSeed
{
    public static async Task SeedResourceTypes(
        WebAppDbContext db,
        IUniqueIdentifierService idService
    )
    {
        // Create 5 new ResourceType entities
        ResourceType[] resourceTypes =
        [
            ResourceType.CreateResourceType(idService.Create(), "Solar"),
            ResourceType.CreateResourceType(idService.Create(), "Wind"),
            ResourceType.CreateResourceType(idService.Create(), "Hydro"),
            ResourceType.CreateResourceType(idService.Create(), "Biomass"),
            ResourceType.CreateResourceType(idService.Create(), "Geothermal")
        ];

        // Add the new ResourceTypes to the DbSet
        await db.ResourceTypes.AddRangeAsync(resourceTypes);

        // Save changes to the database
        await db.SaveChangesAsync();
    }
}
