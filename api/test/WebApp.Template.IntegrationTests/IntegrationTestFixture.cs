using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using WebApp.Template.Application.Data.DbContexts;

namespace WebApp.Template.IntegrationTests;

public class IntegrationTestFixture : AppFixture<Program>
{
    private PostgreSqlContainer _postgres = null!;

    protected override async Task PreSetupAsync()
    {
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("webapp_template_test")
            .Build();

        await _postgres.StartAsync();
    }

    protected override async Task SetupAsync()
    {
        // place one-time setup code here for every test class

        await using var scope = Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();

        await Tools.Modules.Seeding.SeedingModule.Run(db);
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

    // protected override async Task TearDownAsync()
    // {
    // do cleanups here for every test class

    // }

    public WebAppDbContext GetDbContext()
    {
        return Services.GetRequiredService<WebAppDbContext>();
    }
}
