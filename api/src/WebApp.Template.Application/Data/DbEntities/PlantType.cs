using FluentValidation;
using WebApp.Template.Application.Data.Exceptions;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a plant type entity.
/// </summary>
public class PlantType
{
    public long Id { get; init; }
    public string Name { get; init; }

    /* Navigation properties */
    public ICollection<Plant> Plants { get; init; }

    /* Private constructor for EF Core */
    private PlantType() { }

    /// <summary>
    /// Class factory method for creating a new <see cref="PlantType"/> entity.
    /// </summary>
    /// <param name="id">The plant type's id.</param>
    /// <param name="name">The plant type's name.</param>
    /// <returns>A new <see cref="PlantType"/> entity.</returns>
    /// <exception cref="ValidationError">If any of the provided arguments where invalid.</exception>
    public static PlantType CreatePlantType(long id, string name)
    {
        var newType = new PlantType { Id = id, Name = name };

        new PlantTypeValidator().ValidateAndThrow(newType);

        return newType;
    }
}

internal class PlantTypeValidator : AbstractValidator<PlantType>
{
    public PlantTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
