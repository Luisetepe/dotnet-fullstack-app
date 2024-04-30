using Microsoft.EntityFrameworkCore;
using WebApp.Template.Api.Endpoints.Plants;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;

namespace WebApp.Template.IntegrationTests.Features.Plants;

public class GetPlantsListEndpointTests(IntegrationTestFixture fixture) : TestBase<IntegrationTestFixture>
{
    [Theory]
    [InlineData(1, 5, "name", "asc")]
    [InlineData(1, 5, "name", "desc")]
    [InlineData(1, 5, "status", "asc")]
    [InlineData(1, 5, "status", "desc")]
    public async Task Should_Return_Plants(int page, int pageSize, string sortBy, string sortDirection)
    {
        //Arrange
        var expectedPlants = await GetExpectedSearchResult(page, pageSize, sortBy, sortDirection);

        //Act
        var (httpResponse, result) = await fixture.Client.GETAsync<
            GetPlantsListEndpoint,
            GetPlantsListRequest,
            GetPlantsListResponse
        >(
            new GetPlantsListRequest
            {
                PageNumber = page,
                PageSize = pageSize,
                OrderBy = sortBy,
                Order = sortDirection
            }
        );

        //Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().NotBeNull();
        result.Plants.Should().BeEquivalentTo(expectedPlants);
    }

    private async Task<GetPlantsListResponse.Plant[]> GetExpectedSearchResult(
        int page,
        int pageSize,
        string sortBy,
        string sortDirection
    )
    {
        var db = fixture.GetDbContext();
        var plantsQuery = db.Plants.Include(x => x.Status).Include(x => x.Portfolios).AsNoTracking();

        if (sortBy == "name")
        {
            plantsQuery =
                sortDirection == "asc"
                    ? plantsQuery.OrderBy(x => x.Name).ThenBy(x => x.Id)
                    : plantsQuery.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id);
        }
        else if (sortBy == "plantId")
        {
            plantsQuery =
                sortDirection == "asc"
                    ? plantsQuery.OrderBy(x => x.PlantId).ThenBy(x => x.Id)
                    : plantsQuery.OrderByDescending(x => x.PlantId).ThenByDescending(x => x.Id);
        }
        else if (sortBy == "status")
        {
            plantsQuery =
                sortDirection == "asc"
                    ? plantsQuery.OrderBy(x => x.Status.Name).ThenBy(x => x.Id)
                    : plantsQuery.OrderByDescending(x => x.Status.Name).ThenByDescending(x => x.Id);
        }
        else if (sortBy == "capacityDc")
        {
            plantsQuery =
                sortDirection == "asc"
                    ? plantsQuery.OrderBy(x => x.CapacityDc).ThenBy(x => x.Id)
                    : plantsQuery.OrderByDescending(x => x.CapacityDc).ThenByDescending(x => x.Id);
        }

        var expectedPlants = (await plantsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync())
            .Select(x => new GetPlantsListResponse.Plant
            {
                Id = x.Id,
                PlantId = x.PlantId,
                Name = x.Name,
                UtilityCompany = x.UtilityCompany,
                Status = x.Status.Name,
                Tags = x.Tags.Split(',', StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries),
                CapacityDc = x.CapacityDc,
                Portfolios = x.Portfolios.Select(y => y.Name).ToArray()
            })
            .ToArray();

        return expectedPlants;
    }
}
