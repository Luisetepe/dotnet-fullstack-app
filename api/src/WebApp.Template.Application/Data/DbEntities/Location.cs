using FluentValidation;

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
        var newLocation = new Location
        {
            Id = id,
            Name = name,
            Longitude = longitude,
            Latitude = latitude
        };

        new LocationValidator().Validate(newLocation);

        return newLocation;
    }
}

internal class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
    }
}
