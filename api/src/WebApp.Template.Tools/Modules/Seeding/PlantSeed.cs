using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Data.Services;

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

        Plant[] plants =
        [
            Plant.CreatePlant(
                idService.Create(),
                "Plant 1",
                "PL01",
                100,
                1000,
                500,
                "Company 1",
                "Company 1",
                100,
                "Manager 1",
                "TAG01,TAG02",
                "Random notes 1",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 2",
                "PL02",
                200,
                2000,
                1500,
                "Company 2",
                "Company 2",
                200,
                "Manager 2",
                "TAG02,TAG03",
                "Random notes 2",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 3",
                "PL03",
                200,
                2000,
                1500,
                "Company 3",
                "Company 3",
                200,
                "Manager 3",
                "TAG03,TAG04",
                "Random notes 3",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 4",
                "PL04",
                300,
                3000,
                2000,
                "Company 4",
                "Company 4",
                300,
                "Manager 4",
                "TAG04,TAG05",
                "Random notes 4",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 5",
                "PL05",
                400,
                4000,
                2500,
                "Company 5",
                "Company 5",
                400,
                "Manager 5",
                "TAG05,TAG01",
                "Random notes 5",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 6",
                "PL06",
                500,
                5000,
                3000,
                "Company 6",
                "Company 6",
                500,
                "Manager 6",
                "TAG06,TAG02",
                "Random notes 6",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 7",
                "PL07",
                600,
                6000,
                4500,
                "Company 7",
                "Company 7",
                600,
                "Manager 7",
                "TAG07,TAG03",
                "Random notes 7",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 8",
                "PL08",
                700,
                7000,
                5000,
                "Company 8",
                "Company 8",
                700,
                "Manager 8",
                "TAG08,TAG04",
                "Random notes 8",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 9",
                "PL09",
                800,
                8000,
                6500,
                "Company 9",
                "Company 9",
                800,
                "Manager 9",
                "TAG09,TAG05",
                "Random notes 9",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            ),
            Plant.CreatePlant(
                idService.Create(),
                "Plant 10",
                "PL10",
                900,
                9000,
                7000,
                "Company 10",
                "Company 10",
                900,
                "Manager 10",
                "TAG10,TAG01",
                "Random notes 10",
                plantTypes[rand.Next(plantTypes.Length)].Id,
                resourceTypes[rand.Next(resourceTypes.Length)].Id,
                plantStatuses[rand.Next(plantStatuses.Length)].Id,
                locations[rand.Next(locations.Length)].Id,
                Random.Shared.GetItems(portfolios, 2)
            )
        ];

        await db.Plants.AddRangeAsync(plants);

        await db.SaveChangesAsync();
    }
}
