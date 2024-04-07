using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities.Identity;

namespace WebApp.Template.Application.Data.DbEntityConfigurations.Identity;

public class UserRoleRouteConfiguration : IEntityTypeConfiguration<AppRoute>
{
    public void Configure(EntityTypeBuilder<AppRoute> builder)
    {
        builder.ToTable("app_routes");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Path).IsUnique();

        builder.Property(x => x.Id).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Path).HasColumnType("varchar(100)").IsRequired();

        builder.HasMany(x => x.UserRoles).WithMany(x => x.RoleRoutes);
    }
}
