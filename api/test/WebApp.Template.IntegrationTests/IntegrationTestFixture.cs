using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TSID.Creator.NET;
using WebApp.Template.Application.Data.DbContexts;

namespace WebApp.Template.IntegrationTests;

public class IntegrationTestFixture : AppFixture<Program>
{
    private readonly string _testDatabaseId = TsidCreator.GetTsid().ToString();

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
        services.AddDbContext<WebAppDbContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("WebAppDb")!;
            if (!connectionString.Contains("{:id}"))
            {
                throw new ArgumentException("Test database name connection string must contain {:id} placeholder.");
            }

            options.UseNpgsql(connectionString.Replace("{:id}", _testDatabaseId));
            options.UseSnakeCaseNamingConvention();
        });
    }

    protected override async Task TearDownAsync()
    {
        // do cleanups here for every test class

        await using var scope = Services.CreateAsyncScope();

        var db = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();
        await db.Database.EnsureDeletedAsync();
    }

    public WebAppDbContext GetDbContext()
    {
        return Services.GetRequiredService<WebAppDbContext>();
    }
}