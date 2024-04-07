using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;

namespace WebApp.Template.Application.Data.DbEntities.Identity;

public class UserRole
{
    public string Id { get; init; }
    public string Name { get; set; }

    /* Navigation properties */
    public ICollection<User> Users { get; init; }
    public ICollection<AppRoute> RoleRoutes { get; init; }

    /* Private constructor for EF Core */
    private UserRole() { }

    public static Result<UserRole> CreateUserRole(string id, string name)
    {
        var newUserRole = new UserRole { Id = id, Name = name, };

        var validation = new UserRoleValidator().Validate(newUserRole);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newUserRole;
    }
}

internal class UserRoleValidator : AbstractValidator<UserRole>
{
    public UserRoleValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
