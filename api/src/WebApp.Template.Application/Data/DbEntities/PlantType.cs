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
    /// <exception cref="DbEntityCreationException">If any of the provided arguments where invalid.</exception>
    public static PlantType CreatePlantType(long id, string name)
    {
        ValidateCreationArguments(id, name);

        return new PlantType
        {
            Id = id,
            Name = name
        };
    }

    private static void ValidateCreationArguments(long id, string name)
    {
        if (id <= 0)
            throw new DbEntityCreationException(nameof(PlantType), nameof(id));

        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new DbEntityCreationException(nameof(PlantType), nameof(name));
    }
}