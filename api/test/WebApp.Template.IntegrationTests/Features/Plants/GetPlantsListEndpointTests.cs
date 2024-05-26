using Microsoft.EntityFrameworkCore;
using Snapshooter;
using Snapshooter.Xunit;
using WebApp.Template.Api.Endpoints.Plants;
using WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;

namespace WebApp.Template.IntegrationTests.Features.Plants;

public class GetPlantsListEndpointTests(IntegrationTestFixture fixture)
    : TestBase<IntegrationTestFixture>
{
    [Theory]
    [InlineData(1, 5, "name", "asc")]
    [InlineData(1, 5, "name", "desc")]
    [InlineData(1, 5, "status", "asc")]
    [InlineData(1, 5, "status", "desc")]
    public async Task Should_Return_Plants(
        int page,
        int pageSize,
        string sortBy,
        string sortDirection
    )
    {
        // Act
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

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().NotBeNull();
        result
            .Should()
            .MatchSnapshot(
                SnapshotNameExtension.Create(page, pageSize, sortBy, sortDirection),
                matchOptions => matchOptions.IgnoreFields("**.Id")
            );
    }
}
