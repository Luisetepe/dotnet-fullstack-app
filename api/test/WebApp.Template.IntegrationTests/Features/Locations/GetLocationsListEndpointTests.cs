using Snapshooter;
using Snapshooter.Xunit;
using WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;
using WebApp.Template.Endpoints.Locations.GetLocationsList;

namespace WebApp.Template.IntegrationTests.Features.Locations;

public class GetLocationsListEndpointTests(IntegrationTestFixture fixture)
    : TestBase<IntegrationTestFixture>
{
    [Theory]
    [InlineData(1, 5, "name", "asc", "Loc")]
    [InlineData(1, 5, "name", "desc", "")]
    [InlineData(1, 5, "latitude", "asc", "cat")]
    [InlineData(1, 5, "latitude", "desc", "5")]
    [InlineData(1, 5, "longitude", "asc", "on 2")]
    [InlineData(1, 5, "longitude", "desc", "3")]
    public async Task Should_Return_Locations(
        int page,
        int pageSize,
        string sortBy,
        string sortDirection,
        string search
    )
    {
        // Act
        var (httpResponse, result) = await fixture.Client.GETAsync<
            GetLocationsListEndpoint,
            GetLocationsListRequest,
            GetLocationsListResponse
        >(
            new GetLocationsListRequest
            {
                PageNumber = page,
                PageSize = pageSize,
                OrderBy = sortBy,
                Order = sortDirection,
                Search = search
            }
        );

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().NotBeNull();
        result
            .Should()
            .MatchSnapshot(
                SnapshotNameExtension.Create(page, pageSize, sortBy, sortDirection, search),
                matchOptions => matchOptions.IgnoreFields("**.Id")
            );
    }
}
