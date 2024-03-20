using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;
using WebApp.Template.Application.Shared.Models;
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
    public async Task Should_Return_Locations(int page, int pageSize, string sortBy, string sortDirection, string search)
    {
        //Arrange
        var db = fixture.GetDbContext();
        var locationsQuery = db.Locations
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        if (sortBy == "name")
        {
            locationsQuery = locationsQuery.OrderBy(x => x.Name);
        }
        else if (sortBy == "latitude")
        {
            locationsQuery = locationsQuery.OrderBy(x => x.Latitude);
        }
        else if (sortBy == "longitude")
        {
            locationsQuery = locationsQuery.OrderBy(x => x.Longitude);
        }
        if (sortDirection == "desc")
        {
            locationsQuery = locationsQuery.Reverse();
        }
        if (!string.IsNullOrWhiteSpace(search))
        {
            locationsQuery = locationsQuery.Where(x => x.Name.Contains(search));
        }

        var expectedLocations = await locationsQuery.ToArrayAsync();

        // Act
        var (httpResponse, result) = await fixture.Client.GETAsync<GetLocationsListEndpoint, GetLocationsListRequest, GetLocationsListResponse>
        (
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
        result.Status.Should().Be(StatusCode.Success);
        result.Message.Should().BeNull();

        result.Result.Should().NotBeNull();
        result.Result!.Locations.Count().Should().Be(expectedLocations.Length);
    }
}