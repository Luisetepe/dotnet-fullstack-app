using System.Diagnostics.Metrics;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;

namespace WebApp.Template.Application.Data.DbEntities;

/// <summary>
/// Represents a energy plant entity.
/// </summary>
public class Plant
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string PlantId { get; init; }
    public decimal CapacityDc { get; private set; }
    public decimal CapacityAc { get; private set; }
    public decimal StorageCapacity { get; private set; }
    public string ProjectCompany { get; private set; }
    public string UtilityCompany { get; private set; }
    public int Voltage { get; private set; }
    public string AssetManager { get; private set; }
    public string Tags { get; private set; }
    public string? Notes { get; private set; }

    /* Foreign keys */

    public long PlantTypeId { get; private set; }
    public long ResourceTypeId { get; private set; }
    public long StatusId { get; private set; }
    public long LocationId { get; private set; }

    /* Navigational properties */

    public PlantType PlantType { get; init; }
    public ResourceType ResourceType { get; init; }
    public PlantStatus Status { get; init; }
    public Location Location { get; init; }
    public ICollection<Portfolio> Portfolios { get; private set; }

    /* Private constructor for EF Core */
    private Plant() { }

    public static Result<Plant> CreatePlant(
        long id,
        string name,
        string plantId,
        decimal capacityDc,
        decimal capacityAc,
        decimal storageCapacity,
        string projectCompany,
        string utilityCompany,
        int voltage,
        string assetManager,
        string tags,
        string notes,
        long plantTypeId,
        long resourceTypeId,
        long statusId,
        long locationId,
        ICollection<Portfolio>? portfolios = null
    )
    {
        var newPlant = new Plant
        {
            Id = id,
            Name = name,
            PlantId = plantId,
            CapacityDc = capacityDc,
            CapacityAc = capacityAc,
            StorageCapacity = storageCapacity,
            ProjectCompany = projectCompany,
            UtilityCompany = utilityCompany,
            Voltage = voltage,
            AssetManager = assetManager,
            Tags = tags,
            Notes = notes,
            PlantTypeId = plantTypeId,
            ResourceTypeId = resourceTypeId,
            StatusId = statusId,
            LocationId = locationId,
            Portfolios = portfolios ?? new List<Portfolio>()
        };

        var validation = new PlantValidator().Validate(newPlant);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return newPlant;
    }

    public Result UpdatePlant(
        decimal capacityDc,
        decimal capacityAc,
        decimal storageCapacity,
        string projectCompany,
        string utilityCompany,
        int voltage,
        string assetManager,
        string tags,
        string notes,
        long plantTypeId,
        long resourceTypeId,
        long statusId,
        long locationId
    )
    {
        CapacityDc = capacityDc;
        CapacityAc = capacityAc;
        StorageCapacity = storageCapacity;
        ProjectCompany = projectCompany;
        UtilityCompany = utilityCompany;
        Voltage = voltage;
        AssetManager = assetManager;
        Tags = tags;
        Notes = notes;
        PlantTypeId = plantTypeId;
        ResourceTypeId = resourceTypeId;
        StatusId = statusId;
        LocationId = locationId;

        var validation = new PlantValidator().Validate(this);
        if (!validation.IsValid)
        {
            return Result.Invalid(validation.AsErrors());
        }

        return Result.Success();
    }

    public void UpdatePortfolios(ICollection<Portfolio> portfolios)
    {
        Portfolios = portfolios;
    }
}

internal class PlantValidator : AbstractValidator<Plant>
{
    public PlantValidator()
    {
        RuleFor(p => p.Id).GreaterThan(0);
        RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
        RuleFor(p => p.PlantId).NotEmpty().MaximumLength(10);
        RuleFor(p => p.CapacityDc).GreaterThanOrEqualTo(0);
        RuleFor(p => p.CapacityAc).GreaterThanOrEqualTo(0);
        RuleFor(p => p.StorageCapacity).GreaterThanOrEqualTo(0);
        RuleFor(p => p.ProjectCompany).NotEmpty().MaximumLength(100);
        RuleFor(p => p.UtilityCompany).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Voltage).GreaterThanOrEqualTo(0);
        RuleFor(p => p.AssetManager).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Tags).NotEmpty().MaximumLength(150);
        RuleFor(p => p.Notes).MaximumLength(500).When(p => p.Notes != null);
        RuleFor(p => p.PlantTypeId).GreaterThan(0);
        RuleFor(p => p.ResourceTypeId).GreaterThan(0);
        RuleFor(p => p.StatusId).GreaterThan(0);
        RuleFor(p => p.LocationId).GreaterThan(0);
    }
}
