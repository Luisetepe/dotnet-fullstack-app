using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class PortfolioSeed
{
    public static async Task SeedPortfolios(WebAppDbContext db, IUniqueIdentifierService idService)
    {
        // Create 5 new Portfolio entities
        Portfolio[] portfolios =
        [
            Portfolio.CreatePortfolio(idService.Create(), "Portfolio 1"),
            Portfolio.CreatePortfolio(idService.Create(), "Portfolio 2"),
            Portfolio.CreatePortfolio(idService.Create(), "Portfolio 3"),
            Portfolio.CreatePortfolio(idService.Create(), "Portfolio 4"),
            Portfolio.CreatePortfolio(idService.Create(), "Portfolio 5")
        ];

        // Add the new Portfolios to the DbSet
        await db.Portfolios.AddRangeAsync(portfolios);

        // Save changes to the database
        await db.SaveChangesAsync();
    }
}
