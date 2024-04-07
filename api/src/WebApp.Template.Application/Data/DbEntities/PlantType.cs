using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using WebApp.Template.Application.Data.Exceptions;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a plant type entity.
/// </summary>
public class PlantType
{
    public string Id { get; init; }
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
    public static Result<PlantType> CreatePlantType(string id, string name)
    {
        var newType = new PlantType { Id = id, Name = name };

        var validation = new PlantTypeValidator().Validate(newType);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newType;
    }
}

internal class PlantTypeValidator : AbstractValidator<PlantType>
{
    public PlantTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
