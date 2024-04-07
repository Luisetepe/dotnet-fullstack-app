using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using WebApp.Template.Application.Data.Exceptions;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a plant status entity.
/// </summary>
public class PlantStatus
{
    public long Id { get; init; }
    public string Name { get; init; }

    /* Navigation properties */
    public ICollection<Plant> Plants { get; init; }

    /* Private constructor for EF Core */
    private PlantStatus() { }

    /// <summary>
    /// Class factory method for creating a new <see cref="PlantStatus"/> entity.
    /// </summary>
    /// <param name="id">The plant status' id.</param>
    /// <param name="name">The plant status' name.</param>
    /// <returns>A new <see cref="PlantStatus"/> entity.</returns>
    /// <exception cref="ValidationException">If any of the provided arguments where invalid.</exception>
    public static Result<PlantStatus> CreatePlantStatus(long id, string name)
    {
        var newStatus = new PlantStatus { Id = id, Name = name };

        var validation = new PlantStatusValidator().Validate(newStatus);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newStatus;
    }
}

internal class PlantStatusValidator : AbstractValidator<PlantStatus>
{
    public PlantStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
