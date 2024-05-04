using Bogus;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class LocationSeed
{
    public static async Task SeedLocations(WebAppDbContext db, IUniqueIdentifierService idService)
    {
        var locationsFaker = new Faker<Location>().CustomInstantiator(f =>
            Location.CreateLocation(
                idService.Create(),
                f.Address.City(),
                f.Address.Longitude(),
                f.Address.Latitude()
            )
        );

        // Create 5 new Location entities
        var locations = locationsFaker.Generate(20);

        // Add the new Locations to the DbSet
        await db.Locations.AddRangeAsync(locations);

        // Save changes to the database
        await db.SaveChangesAsync();
    }
}
