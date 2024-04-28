using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using FluentValidation;
using MediatR;

namespace WebApp.Template.Application.Features.Identity.Queries.CheckAllowedRoute;

public class CheckAllowedRouteQuery : IRequest<Result>
{
    public CheckAllowedRouteRequest Request { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public record CheckAllowedRouteRequest
{
    public string Route { get; set; }
}

public class CheckAllowedRouteRequestValidator : Validator<CheckAllowedRouteRequest>
{
    public CheckAllowedRouteRequestValidator()
    {
        RuleFor(x => x.Route).NotEmpty().WithMessage("The 'route' field is required.");
    }
}
