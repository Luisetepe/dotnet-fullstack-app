using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;

namespace WebApp.Template.Application.Data.DbEntities.Identity;

public class UserSession
{
    public string Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ExpiresAt { get; init; }

    /* Foreign keys */
    public string UserId { get; init; }

    /* Navigation properties */
    public User User { get; init; }

    /* Private constructor for EF Core */
    private UserSession() { }

    public static Result<UserSession> CreateUserSession(
        string id,
        DateTime createdAt,
        DateTime expiresAt,
        string userId
    )
    {
        var newUserSession = new UserSession
        {
            Id = id,
            CreatedAt = createdAt,
            ExpiresAt = expiresAt,
            UserId = userId
        };

        var validation = new UserSessionValidator().Validate(newUserSession);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newUserSession;
    }
}

internal class UserSessionValidator : AbstractValidator<UserSession>
{
    public UserSessionValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.CreatedAt).NotEmpty();
        RuleFor(x => x.ExpiresAt).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
