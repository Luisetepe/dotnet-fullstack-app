using WebApp.Template.Api.Endpoints.Dashboard;

namespace WebApp.Template.IntegrationTests.Features.Dashboard
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;
    using WebApp.Template.Application.Shared.Models;
    using Xunit;

    public class GetDashboardWidgetsDataEndpointTests(IntegrationTestFixture fixture, ITestOutputHelper outputHelper)
        : TestClass<IntegrationTestFixture>(fixture, outputHelper)
    {

        [Fact]
        public async Task Should_Return_Dashboard_Widgets_Data()
        {
            var db = Fixture.GetDbContext();

            // Arrange
            var expectedLocations = await db.Locations.CountAsync();
            var expectedPlants = await db.Plants.CountAsync();
            var expectedSolarCapacity = await db.Plants.SumAsync(p => p.CapacityDc);
            var expectedStorageCapacity = await db.Plants.SumAsync(p => p.StorageCapacity);

            // Act
            var (httpResponse, result) = await Fixture.Client.GETAsync<GetDashboardWidgetsDataEndpoint, GetDashboardWidgetsDataResponse>();

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            result.Should().NotBeNull();
            result.Status.Should().Be(StatusCode.Success);
            result.Message.Should().BeNull();

            result.Result.Should().NotBeNull();
            result.Result!.Locations.Should().Be(expectedLocations);
            result.Result!.Plants.Should().Be(expectedPlants);
            result.Result!.SolarCapacity.Should().Be(expectedSolarCapacity);
            result.Result!.StorageCapacity.Should().Be(expectedStorageCapacity);
        }
    }
}