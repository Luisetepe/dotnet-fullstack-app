using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using WebApp.Template.Application.Data.Exceptions;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a portfolio entity.
/// </summary>
public class Portfolio
{
    public string Id { get; init; }
    public string Name { get; init; }

    /* Navigation properties */
    public ICollection<Plant> Plants { get; init; }

    /* Private constructor for EF Core */
    private Portfolio() { }

    /// <summary>
    /// Class factory method for creating a new <see cref="Portfolio"/> entity.
    /// </summary>
    /// <param name="id">The portfolio's id.</param>
    /// <param name="name">The portfolio's name.</param>
    /// <returns>A new <see cref="Portfolio"/> entity.</returns>
    /// <exception cref="ValidationError">If any of the provided arguments where invalid.</exception>
    public static Result<Portfolio> CreatePortfolio(string id, string name)
    {
        var newPortfolio = new Portfolio { Id = id, Name = name };

        var validation = new PortfolioValidator().Validate(newPortfolio);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newPortfolio;
    }
}

internal class PortfolioValidator : AbstractValidator<Portfolio>
{
    public PortfolioValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
