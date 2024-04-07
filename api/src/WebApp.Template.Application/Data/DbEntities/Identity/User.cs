using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;

namespace WebApp.Template.Application.Data.DbEntities.Identity;

public class User
{
    public string Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }

    /* Foreign keys */
    public string RoleId { get; set; }

    /* Navigation properties */
    public UserRole UserRole { get; private set; }
    public ICollection<UserSession> UserSessions { get; private set; }

    /* Private constructor for EF Core */
    private User() { }

    public static Result<User> CreateUser(
        string id,
        string userName,
        string email,
        string passwordHash,
        string PasswordSalt,
        string roleId
    )
    {
        var newUser = new User
        {
            Id = id,
            UserName = userName,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = PasswordSalt,
            RoleId = roleId
        };

        var validation = new UserValidator().Validate(newUser);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newUser;
    }
}

internal class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PasswordHash).NotEmpty();
        RuleFor(x => x.PasswordSalt).NotEmpty();
        RuleFor(x => x.RoleId).NotEmpty();
    }
}
