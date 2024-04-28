using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;
using WebApp.Template.Endpoints.Locations.GetLocationsList;

namespace WebApp.Template.IntegrationTests.Features.Locations;

public class GetLocationsListEndpointTests(IntegrationTestFixture fixture) : TestBase<IntegrationTestFixture>
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
        //Arrange
        var db = fixture.GetDbContext();
        var locationsQuery = db
            .Locations.AsNoTracking()
            .Where(x => string.IsNullOrWhiteSpace(search) || x.Name.Contains(search));

        if (sortBy == "name")
        {
            locationsQuery =
                sortDirection == "asc"
                    ? locationsQuery.OrderBy(x => x.Name).ThenBy(x => x.Id)
                    : locationsQuery.OrderByDescending(x => x.Name).ThenBy(x => x.Id);
        }
        else if (sortBy == "latitude")
        {
            locationsQuery =
                sortDirection == "asc"
                    ? locationsQuery.OrderBy(x => x.Latitude).ThenBy(x => x.Id)
                    : locationsQuery.OrderByDescending(x => x.Latitude).ThenBy(x => x.Id);
        }
        else if (sortBy == "longitude")
        {
            locationsQuery =
                sortDirection == "asc"
                    ? locationsQuery.OrderBy(x => x.Longitude).ThenBy(x => x.Id)
                    : locationsQuery.OrderByDescending(x => x.Longitude).ThenBy(x => x.Id);
        }

        var expectedLocations = (await locationsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync())
            .Select(x => new GetLocationsListResponse.Location
            {
                Id = x.Id,
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            })
            .ToArray();

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
        result.Locations.Should().BeEquivalentTo(expectedLocations);
    }
}
