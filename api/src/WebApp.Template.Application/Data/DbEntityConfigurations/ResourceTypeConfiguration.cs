using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities;

namespace WebApp.Template.Application.Data.DbEntityConfigurations;

public class ResourceTypeConfiguration : IEntityTypeConfiguration<ResourceType>
{
    public void Configure(EntityTypeBuilder<ResourceType> builder)
    {
        builder.ToTable("resource_types");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("bigint")
            .IsRequired();
        builder.Property(x => x.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();
    }
}