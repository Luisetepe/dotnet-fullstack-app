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
    /// <exception cref="DbEntityCreationException">If any of the provided arguments where invalid.</exception>
    public static PlantStatus CreatePlantStatus(long id, string name)
    {
        ValidateCreationArguments(id, name);

        return new PlantStatus { Id = id, Name = name };
    }

    private static void ValidateCreationArguments(long id, string name)
    {
        if (id <= 0)
            throw new DbEntityCreationException(nameof(PlantStatus), nameof(id));

        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new DbEntityCreationException(nameof(PlantStatus), nameof(name));
    }
}
