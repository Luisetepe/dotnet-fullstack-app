using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities.Identity;
using WebApp.Template.Application.Services.Crypto;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Tools.Modules.Seeding;

public static class IdentitySeed
{
    public static async Task SeedUsers(
        WebAppDbContext db,
        IUniqueIdentifierService idService,
        ICryptoService cryptoService
    )
    {
        UserRole[] roles =
        [
            UserRole.CreateUserRole(idService.Create(), "Admin"),
            UserRole.CreateUserRole(idService.Create(), "Regular")
        ];

        AppRoute[] roleRoutes =
        [
            AppRoute.CreateUserRoleRoute(idService.Create(), "Dashboard", "/home/dashboard", roles),
            AppRoute.CreateUserRoleRoute(idService.Create(), "Locations", "/home/assets/locations", roles),
            AppRoute.CreateUserRoleRoute(idService.Create(), "Plants", "/home/assets/plants", [roles[0]]),
        ];

        User[] users = Enumerable
            .Range(0, 2)
            .Select(i =>
            {
                var userRole = roles[i];
                var salt = cryptoService.GenerateSalt();
                var password = cryptoService.HashPassword("Pa$$w0rd.123", salt);
                var userName = i == 0 ? "Admin User" : "Regular User";
                var email = i == 0 ? "admin.user@webapp.com" : "regular.user@webapp.com";

                var result = User.CreateUser(
                    idService.Create(),
                    userName,
                    email,
                    password,
                    Convert.ToBase64String(salt),
                    userRole.Id
                );

                return result.Value;
            })
            .ToArray();

        await db.UserRoles.AddRangeAsync(roles);
        await db.AppRoutes.AddRangeAsync(roleRoutes);
        await db.Users.AddRangeAsync(users);

        await db.SaveChangesAsync();
    }
}
