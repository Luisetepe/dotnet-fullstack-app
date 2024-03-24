using WebApp.Template.Application.Data.Exceptions;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a resource type entity.
/// </summary>
public class ResourceType
{
    public long Id { get; init; }
    public string Name { get; init; }

    /* Navigation properties */
    public ICollection<Plant> Plants { get; init; }

    /* Private constructor for EF Core */
    private ResourceType() { }

    /// <summary>
    /// Class factory method for creating a new <see cref="ResourceType"/> entity.
    /// </summary>
    /// <param name="id">The resource type's id.</param>
    /// <param name="name">The resource type's name.</param>
    /// <returns>A new <see cref="ResourceType"/> entity.</returns>
    /// <exception cref="DbEntityCreationException">If any of the provided arguments where invalid.</exception>
    public static ResourceType CreateResourceType(long id, string name)
    {
        ValidateCreationArguments(id, name);

        return new ResourceType { Id = id, Name = name };
    }

    private static void ValidateCreationArguments(long id, string name)
    {
        if (id <= 0)
            throw new DbEntityCreationException(nameof(ResourceType), nameof(id));

        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new DbEntityCreationException(nameof(ResourceType), nameof(name));
    }
}
