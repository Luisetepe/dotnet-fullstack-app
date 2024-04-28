using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities;

namespace WebApp.Template.Application.Data.DbEntityConfigurations;

public class PlantConfiguration : IEntityTypeConfiguration<Plant>
{
    public void Configure(EntityTypeBuilder<Plant> builder)
    {
        builder.ToTable("plants");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Id).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.PlantId).HasColumnType("varchar(20)").IsRequired();
        builder.Property(x => x.ProjectCompany).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.UtilityCompany).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.Tags).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.AssetManager).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.Notes).HasColumnType("varchar(500)").IsRequired();
        builder.Property(x => x.CapacityDc).HasColumnType("numeric").IsRequired();
        builder.Property(x => x.CapacityAc).HasColumnType("numeric").IsRequired();

        /* Foreign keys */

        builder.Property(x => x.StatusId).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.LocationId).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.PlantTypeId).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.ResourceTypeId).HasColumnType("varchar(50)").IsRequired();

        /* Relationships */

        builder
            .HasOne(x => x.Status)
            .WithMany(x => x.Plants)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Location).WithMany(x => x.Plants).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.PlantType).WithMany(x => x.Plants).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ResourceType).WithMany(x => x.Plants).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Portfolios).WithMany(x => x.Plants);
    }
}
