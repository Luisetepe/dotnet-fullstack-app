using WebApp.Template.Api.Endpoints.Dashboard;

namespace WebApp.Template.IntegrationTests.Features.Dashboard
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;
    using Xunit;

    public class GetDashboardWidgetsDataEndpointTests(IntegrationTestFixture fixture)
        : TestBase<IntegrationTestFixture>
    {
        [Fact]
        public async Task Should_Return_Dashboard_Widgets_Data()
        {
            var db = fixture.GetDbContext();

            // Arrange
            var expectedLocations = await db.Locations.CountAsync();
            var expectedPlants = await db.Plants.CountAsync();
            var expectedSolarCapacity = await db.Plants.SumAsync(p => p.CapacityDc);
            var expectedStorageCapacity = await db.Plants.SumAsync(p => p.StorageCapacity);

            // Act
            var (httpResponse, result) = await fixture.Client.GETAsync<
                GetDashboardWidgetsDataEndpoint,
                GetDashboardWidgetsDataResponse
            >();

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            result.Should().NotBeNull();
            result.Locations.Should().Be(expectedLocations);
            result.Plants.Should().Be(expectedPlants);
            result.SolarCapacity.Should().Be(expectedSolarCapacity);
            result.StorageCapacity.Should().Be(expectedStorageCapacity);
        }
    }
}
