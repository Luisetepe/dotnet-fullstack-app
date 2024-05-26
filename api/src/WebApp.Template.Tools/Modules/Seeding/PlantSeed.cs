using Bogus;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class PlantSeed
{
    public static async Task SeedPlants(WebAppDbContext db, IUniqueIdentifierService idService)
    {
        var rand = new Random();

        var locations = db.Locations.ToArray();
        if (locations.Length == 0)
            throw new Exception("Locations must be seeded before Plants.");
        var plantStatuses = db.PlantStatuses.ToArray();
        if (plantStatuses.Length == 0)
            throw new Exception("PlantStatuses must be seeded before Plants.");
        var plantTypes = db.PlantTypes.ToArray();
        if (plantTypes.Length == 0)
            throw new Exception("PlantTypes must be seeded before Plants.");
        var resourceTypes = db.ResourceTypes.ToArray();
        if (resourceTypes.Length == 0)
            throw new Exception("ResourceTypes must be seeded before Plants.");
        var portfolios = db.Portfolios.ToArray();
        if (portfolios.Length < 2)
            throw new Exception("Portfolios must be seeded before Plants.");

        var plantFaker = new Faker<Plant>().CustomInstantiator(f =>
            Plant.CreatePlant(
                idService.Create(),
                f.Random.Words(3),
                f.Random.AlphaNumeric(5),
                f.Random.Number(100, 1000),
                f.Random.Number(1000, 10000),
                f.Random.Number(500, 5000),
                f.Company.CompanyName(),
                f.Company.CompanyName(),
                f.Random.Number(100, 1000),
                f.Name.FullName(),
                f.Random.Words(2),
                f.Lorem.Sentence(),
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            )
        );

        var plants = plantFaker.Generate(1000);

        await db.Plants.AddRangeAsync(plants);

        await db.SaveChangesAsync();
    }
}
