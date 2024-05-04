using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities.Identity;
using WebApp.Template.Application.Services.Crypto;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Application.Features.Identity.Commands.SignInUser;

public class SignInUserHandler(
    WebAppDbContext dbContext,
    ICryptoService cryptoService,
    IUniqueIdentifierService idService
) : IRequestHandler<SignInUserCommand, Result<SignInUserResponse>>
{
    public async Task<Result<SignInUserResponse>> Handle(
        SignInUserCommand command,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await dbContext
                .Users.Include(u => u.UserRole)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == command.Request.Email, cancellationToken);

            if (user == null)
            {
                return Result.NotFound($"User with email {command.Request.Email} not found.");
            }

            if (
                !cryptoService.VeryfyPassword(
                    command.Request.Password,
                    user.PasswordHash,
                    Convert.FromBase64String(user.PasswordSalt)
                )
            )
            {
                return Result.Unauthorized();
            }

            var roleRoutes = await dbContext
                .AppRoutes.Include(r => r.UserRoles)
                .AsNoTracking()
                .Where(r => r.UserRoles.Any(ur => ur.Id == user.RoleId))
                .ToArrayAsync(cancellationToken);

            var sessionResult = UserSession.CreateUserSession(
                idService.Create(),
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                user.Id
            );

            if (!sessionResult.IsSuccess)
            {
                return Result.Invalid(sessionResult.ValidationErrors.ToArray());
            }

            dbContext.UserSessions.Add(sessionResult.Value);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(
                new SignInUserResponse
                {
                    SessionId = sessionResult.Value.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Role = user.UserRole.Name,
                    RoleId = user.UserRole.Id,
                    AllowedRoutes = roleRoutes.Select(r => r.Path).ToArray()
                }
            );
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}
