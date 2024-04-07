using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Template.Application.Data.DbEntities.Identity;

namespace WebApp.Template.Application.Data.DbEntityConfigurations.Identity;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Id).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Email).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.UserName).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.PasswordSalt).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.RoleId).HasColumnType("varchar(50)").IsRequired();

        builder
            .HasOne(x => x.UserRole)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
