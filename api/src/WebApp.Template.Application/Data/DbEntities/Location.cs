using WebApp.Template.Application.Data.Exceptions;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a location entity.
/// </summary>
public class Location
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Longitude { get; init; }
    public decimal Latitude { get; init; }

    /* Navigation properties */
    public ICollection<Plant> Plants { get; init; }

    /* Private constructor for EF Core */
    private Location() { }

    public static Location CreateLocation(long id, string name, decimal longitude, decimal latitude)
    {
        ValidateCreationArguments(id, name, longitude, latitude);

        return new Location
        {
            Id = id,
            Name = name,
            Longitude = longitude,
            Latitude = latitude
        };
    }

    private static void ValidateCreationArguments(long id, string name, decimal longitude, decimal latitude)
    {
        if (id <= 0)
            throw new DbEntityCreationException(nameof(Location), nameof(id));

        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new DbEntityCreationException(nameof(Location), nameof(name));

        if (longitude is < -180 or > 180)
            throw new DbEntityCreationException(nameof(Location), nameof(longitude));

        if (latitude is < -90 or > 90)
            throw new DbEntityCreationException(nameof(Location), nameof(latitude));
    }
}