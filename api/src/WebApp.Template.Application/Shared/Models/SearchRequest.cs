using FastEndpoints;
using FluentValidation;

namespace WebApp.Template.Application.Shared.Models;

public record SearchRequest
{
    public int PageSize { get; init; } = 10;
    public int PageNumber { get; init; } = 1;
    public string? OrderBy { get; init; }
    public string? Order { get; init; } = "asc";
    public string? Search { get; init; }
}

public abstract class SearchRequestValidator<T> : Validator<T>
    where T : SearchRequest
{
    private readonly string[] _validSortDirections = ["asc", "desc"];

    public SearchRequestValidator()
    {
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("pageSize must be greater than 0");
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("pageNumber must be greater than 0");
        RuleFor(x => x.Order)
            .Must(x => _validSortDirections.Contains(x))
            .WithMessage("order must be 'asc' or 'desc'");
    }
}
