using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;

namespace WebApp.Template.Application.Features.Identity.Queries.CheckAllowedRoute;

public class CheckAllowedRouteHandler(WebAppDbContext dbContext)
    : IRequestHandler<CheckAllowedRouteQuery, Result>
{
    public async Task<Result> Handle(
        CheckAllowedRouteQuery query,
        CancellationToken cancellationToken
    )
    {
        var route = await dbContext
            .AppRoutes.Include(r => r.UserRoles)
            .Select(r => new { r.Path, r.UserRoles })
            .AsNoTracking()
            .SingleOrDefaultAsync(
                r => r.Path.Equals(query.Request.Route, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken
            );

        if (route == null)
        {
            return Result.NotFound($"Route {query.Request.Route} not found.");
        }

        if (!route.UserRoles.Any(ur => ur.Id == query.User.FindFirst("RoleId")?.Value))
        {
            return Result.Forbidden();
        }

        return Result.Success();
    }
}
