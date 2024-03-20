using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbEntities;

namespace WebApp.Template.Application.Data.DbContexts;

public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : DbContext(options)
{
    public DbSet<Plant> Plants { get; set; }
    public DbSet<PlantType> PlantTypes { get; set; }
    public DbSet<ResourceType> ResourceTypes { get; set; }
    public DbSet<PlantStatus> PlantStatuses { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebAppDbContext).Assembly);
    }
}