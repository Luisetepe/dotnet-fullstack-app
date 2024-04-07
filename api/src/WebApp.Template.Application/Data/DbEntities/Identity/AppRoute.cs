using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;

namespace WebApp.Template.Application.Data.DbEntities.Identity;

public class AppRoute
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }

    /* Navigation Properties */
    public ICollection<UserRole> UserRoles { get; set; }

    public static Result<AppRoute> CreateUserRoleRoute(
        string id,
        string name,
        string path,
        ICollection<UserRole>? userRoles = null
    )
    {
        var newUserRoleRoute = new AppRoute
        {
            Id = id,
            Name = name,
            Path = path,
            UserRoles = userRoles ?? []
        };

        var validation = new UserRoleRouteValidator().Validate(newUserRoleRoute);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newUserRoleRoute;
    }
}

internal class UserRoleRouteValidator : AbstractValidator<AppRoute>
{
    public UserRoleRouteValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Path).NotEmpty().MaximumLength(100);
    }
}
