using Microsoft.EntityFrameworkCore;
using WebApp.Template.Api.Endpoints.Plants;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.IntegrationTests.Features.Plants;

public class GetPlantsListEndpointTests(IntegrationTestFixture fixture)
    : TestBase<IntegrationTestFixture>
{

    [Theory]
    [InlineData(1, 5, "name", "asc", "ant")]
    [InlineData(1, 5, "name", "desc", "")]
    [InlineData(1, 5, "status", "asc", "")]
    [InlineData(1, 5, "status", "desc", "5")]
    public async Task Should_Return_Plants(int page, int pageSize, string sortBy, string sortDirection, string search)
    {
        //Arrange
        var db = fixture.GetDbContext();
        var plantsQuery = db.Plants
            .Include(x => x.Status)
            .Include(x => x.Portfolios)
            .AsNoTracking()
            .Where(x => string.IsNullOrWhiteSpace(search) || x.Name.Contains(search));


        if (sortBy == "name")
        {
            plantsQuery = plantsQuery.OrderBy(x => x.Name);
        }
        else if (sortBy == "plantId")
        {
            plantsQuery = plantsQuery.OrderBy(x => x.PlantId);
        }
        else if (sortBy == "status")
        {
            plantsQuery = plantsQuery.OrderBy(x => x.Status.Name);
        }
        else if (sortBy == "capacityDc")
        {
            plantsQuery = plantsQuery.OrderBy(x => x.CapacityDc);
        }
        if (sortDirection == "desc")
        {
            plantsQuery = plantsQuery.Reverse();
        }

        var expectedPlants = await plantsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync();


        //Act
        var (httpResponse, result) = await fixture.Client.GETAsync<GetPlantsListEndpoint, GetPlantsListRequest, GetPlantsListResponse>
        (
            new GetPlantsListRequest
            {
                PageNumber = page,
                PageSize = pageSize,
                OrderBy = sortBy,
                Order = sortDirection,
                Search = search
            }
        );

        //Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().NotBeNull();
        result.Status.Should().Be(StatusCode.Success);
        result.Message.Should().BeNull();

        result.Result.Should().NotBeNull();
        result.Result!.Plants.Count().Should().Be(expectedPlants.Length);

        // var index = 0;
        // foreach (var plant in result.Result!)
        // {
        //     var expectedPlant = expectedPlants[index++];
        //     expectedPlant.Should().NotBeNull();
        //     plant.Name.Should().Be(expectedPlant.Name);
        //     plant.PlantId.Should().Be(expectedPlant.PlantId);
        //     plant.UtilityCompany.Should().Be(expectedPlant.UtilityCompany);
        //     plant.Status.Should().Be(expectedPlant.Status.Name);
        //     plant.Tags.Should().BeEquivalentTo(expectedPlant.Tags.Split(',', StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries));
        //     plant.CapacityDc.Should().Be(expectedPlant.CapacityDc);
        //     plant.Portfolios.Should().BeEquivalentTo(expectedPlant.Portfolios.Select(y => y.Name).ToArray());
        // }
    }
}