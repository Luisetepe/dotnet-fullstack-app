using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.IntegrationTests.TestData
{
    public class TestSeeder
    {
        public static void SeedTestData(WebAppDbContext context, IUniqueIdentifierService idService)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Location[] locations =
            [
                Location.CreateLocation(idService.Create(), "Location 1", 150.123456, -34.123456),
                Location.CreateLocation(idService.Create(), "Location 2", -120.654321, 45.654321),
                Location.CreateLocation(idService.Create(), "Location 3", -179.987654, 89.987654),
                Location.CreateLocation(idService.Create(), "Location 4", 178.765432, -88.765432),
                Location.CreateLocation(idService.Create(), "Location 5", 78.947586, -43.058302)
            ];
            Portfolio[] portfolios =
            [
                Portfolio.CreatePortfolio(idService.Create(), "Portfolio 1"),
                Portfolio.CreatePortfolio(idService.Create(), "Portfolio 2"),
                Portfolio.CreatePortfolio(idService.Create(), "Portfolio 3"),
                Portfolio.CreatePortfolio(idService.Create(), "Portfolio 4"),
                Portfolio.CreatePortfolio(idService.Create(), "Portfolio 5")
            ];
            PlantStatus[] plantStatuses =
            [
                PlantStatus.CreatePlantStatus(idService.Create(), "Operational"),
                PlantStatus.CreatePlantStatus(idService.Create(), "Under construction"),
                PlantStatus.CreatePlantStatus(idService.Create(), "Decommissioned"),
                PlantStatus.CreatePlantStatus(idService.Create(), "Planned"),
                PlantStatus.CreatePlantStatus(idService.Create(), "Closed")
            ];
            PlantType[] plantTypes =
            [
                PlantType.CreatePlantType(idService.Create(), "Ground mount"),
                PlantType.CreatePlantType(idService.Create(), "Roof top"),
                PlantType.CreatePlantType(idService.Create(), "Floating"),
                PlantType.CreatePlantType(idService.Create(), "Hybrid"),
                PlantType.CreatePlantType(idService.Create(), "Other")
            ];
            ResourceType[] resourceTypes =
            [
                ResourceType.CreateResourceType(idService.Create(), "Solar"),
                ResourceType.CreateResourceType(idService.Create(), "Wind"),
                ResourceType.CreateResourceType(idService.Create(), "Hydro"),
                ResourceType.CreateResourceType(idService.Create(), "Geothermal"),
                ResourceType.CreateResourceType(idService.Create(), "Biomass")
            ];
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
                    plantTypes[0].Id,
                    resourceTypes[0].Id,
                    plantStatuses[0].Id,
                    locations[0].Id,
                    portfolios[0..2]
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
                    plantTypes[1].Id,
                    resourceTypes[1].Id,
                    plantStatuses[1].Id,
                    locations[1].Id,
                    portfolios[1..3]
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
                    plantTypes[2].Id,
                    resourceTypes[2].Id,
                    plantStatuses[2].Id,
                    locations[2].Id,
                    portfolios[2..4]
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
                    plantTypes[3].Id,
                    resourceTypes[3].Id,
                    plantStatuses[3].Id,
                    locations[3].Id,
                    portfolios[3..5]
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
                    plantTypes[4].Id,
                    resourceTypes[4].Id,
                    plantStatuses[4].Id,
                    locations[4].Id,
                    [portfolios[4]]
                )
            ];

            context.Locations.AddRange(locations);
            context.Portfolios.AddRange(portfolios);
            context.PlantStatuses.AddRange(plantStatuses);
            context.PlantTypes.AddRange(plantTypes);
            context.ResourceTypes.AddRange(resourceTypes);
            context.Plants.AddRange(plants);

            context.SaveChanges();
        }
    }
}
