using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Services.Identity;
using WebApp.Template.IntegrationTests.TestData;

namespace WebApp.Template.IntegrationTests;

public class IntegrationTestFixture : AppFixture<Program>
{
    private const string dbImage = "postgres:16-alpine";
    private const string dbName = "webapp_template_test";

    private PostgreSqlContainer _postgres = null!;

    protected override async Task PreSetupAsync()
    {
        _postgres = new PostgreSqlBuilder().WithImage(dbImage).WithDatabase(dbName).Build();

        await _postgres.StartAsync();
    }

    protected override async Task SetupAsync()
    {
        // place one-time setup code here for every test class

        await using var scope = Services.CreateAsyncScope();

        TestSeeder.SeedTestData(
            scope.ServiceProvider.GetRequiredService<WebAppDbContext>(),
            scope.ServiceProvider.GetRequiredService<IUniqueIdentifierService>()
        );
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        // replace the real database with a test database
        services.RemoveAll<DbContextOptions<WebAppDbContext>>();
        services.RemoveAll<WebAppDbContext>();
        services.AddDbContext<WebAppDbContext>(
            (options) =>
            {
                options.UseNpgsql(_postgres.GetConnectionString());
                options.UseSnakeCaseNamingConvention();
            }
        );
    }

    protected override Task TearDownAsync()
    {
        // do cleanups here for every test class

        return Task.CompletedTask;
    }

    public WebAppDbContext GetDbContext()
    {
        return Services.GetRequiredService<WebAppDbContext>();
    }
}
