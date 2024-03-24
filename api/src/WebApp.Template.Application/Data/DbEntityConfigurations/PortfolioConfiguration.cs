using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities;

namespace WebApp.Template.Application.Data.DbEntityConfigurations;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("portfolios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnType("bigint").IsRequired();
        builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();
    }
}
