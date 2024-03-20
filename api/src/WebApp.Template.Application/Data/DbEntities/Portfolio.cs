using WebApp.Template.Application.Data.Exceptions;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a portfolio entity.
/// </summary>
public class Portfolio
{
    public long Id { get; init; }
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
    /// <exception cref="DbEntityCreationException">If any of the provided arguments where invalid.</exception>
    public static Portfolio CreatePortfolio(long id, string name)
    {
        ValidateCreationArguments(id, name);

        return new Portfolio
        {
            Id = id,
            Name = name
        };
    }

    private static void ValidateCreationArguments(long id, string name)
    {
        if (id <= 0)
            throw new DbEntityCreationException(nameof(Portfolio), nameof(id));

        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new DbEntityCreationException(nameof(Portfolio), nameof(name));
    }
}