using WebApp.Template.Api.Endpoints.Dashboard;

namespace WebApp.Template.IntegrationTests.Features.Dashboard
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Snapshooter.Xunit;
    using WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;
    using Xunit;

    public class GetDashboardWidgetsDataEndpointTests(IntegrationTestFixture fixture)
        : TestBase<IntegrationTestFixture>
    {
        [Fact]
        public async Task Should_Return_Dashboard_Widgets_Data()
        {
            // Act
            var (httpResponse, result) = await fixture.Client.GETAsync<
                GetDashboardWidgetsDataEndpoint,
                GetDashboardWidgetsDataResponse
            >();

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            result.Should().NotBeNull();
            result.Should().MatchSnapshot(matchOptions => matchOptions.IgnoreFields("**.Id"));
        }
    }
}
