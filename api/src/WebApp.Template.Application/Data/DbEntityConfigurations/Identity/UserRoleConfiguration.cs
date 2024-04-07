using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities.Identity;

namespace WebApp.Template.Application.Data.DbEntityConfigurations.Identity;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Id).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();

        builder.HasMany(x => x.RoleRoutes).WithMany(x => x.UserRoles);
    }
}
