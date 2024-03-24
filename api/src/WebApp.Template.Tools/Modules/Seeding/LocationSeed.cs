using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Data.Services;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class LocationSeed
{
    public static async Task SeedLocations(WebAppDbContext db, IUniqueIdentifierService idService)
    {
        // Create 5 new Location entities
        Location[] locations =
        [
            Location.CreateLocation(idService.Create(), "Location 1", 150.123456m, -34.123456m),
            Location.CreateLocation(idService.Create(), "Location 2", -120.654321m, 45.654321m),
            Location.CreateLocation(idService.Create(), "Location 3", -179.987654m, 89.987654m),
            Location.CreateLocation(idService.Create(), "Location 4", 178.765432m, -88.765432m),
            Location.CreateLocation(idService.Create(), "Location 5", 78.947586m, -43.058302m),
        ];

        // Add the new Locations to the DbSet
        await db.Locations.AddRangeAsync(locations);

        // Save changes to the database
        await db.SaveChangesAsync();
    }
}
