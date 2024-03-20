using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities;

namespace WebApp.Template.Application.Data.DbEntityConfigurations;

public class PlantStatusConfiguration : IEntityTypeConfiguration<PlantStatus>
{
    public void Configure(EntityTypeBuilder<PlantStatus> builder)
    {
        builder.ToTable("plant_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("bigint")
            .IsRequired();
        builder.Property(x => x.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();
    }
}