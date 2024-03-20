using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities;

namespace WebApp.Template.Application.Data.DbEntityConfigurations;

public class PlantTypeConfiguration : IEntityTypeConfiguration<PlantType>
{
    public void Configure(EntityTypeBuilder<PlantType> builder)
    {
        builder.ToTable("plant_types");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("bigint")
            .IsRequired();
        builder.Property(x => x.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();
    }
}