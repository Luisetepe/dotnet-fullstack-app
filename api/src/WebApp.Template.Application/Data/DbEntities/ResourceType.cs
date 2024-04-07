using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
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
    public static Result<ResourceType> CreateResourceType(long id, string name)
    {
        var newResource = new ResourceType { Id = id, Name = name };

        var validation = new ResourceTypeValidator().Validate(newResource);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newResource;
    }
}

internal class ResourceTypeValidator : AbstractValidator<ResourceType>
{
    public ResourceTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
