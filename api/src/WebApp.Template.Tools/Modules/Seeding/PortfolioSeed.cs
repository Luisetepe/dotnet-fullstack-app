using Bogus;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class PortfolioSeed
{
    public static async Task SeedPortfolios(WebAppDbContext db, IUniqueIdentifierService idService)
    {
        var PortfolioFaker = new Faker<Portfolio>().CustomInstantiator(f =>
            Portfolio.CreatePortfolio(idService.Create(), f.Company.CompanyName())
        );

        var portfolios = PortfolioFaker.Generate(20);

        // Add the new Portfolios to the DbSet
        await db.Portfolios.AddRangeAsync(portfolios);

        // Save changes to the database
        await db.SaveChangesAsync();
    }
}
