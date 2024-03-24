using WebApp.Template.Application.Data.Exceptions;

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
    public string Notes { get; private set; }

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
    public ICollection<Portfolio> Portfolios { get; init; }

    /* Private constructor for EF Core */
    private Plant() { }

    public static Plant CreatePlant(
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
        ValidateCreationArguments(
            id,
            name,
            plantId,
            capacityDc,
            capacityAc,
            storageCapacity,
            projectCompany,
            utilityCompany,
            voltage,
            assetManager,
            tags,
            notes,
            plantTypeId,
            resourceTypeId,
            statusId,
            locationId
        );

        return new Plant
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
    }

    private static void ValidateCreationArguments(
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
        long locationId
    )
    {
        if (id <= 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(Id));

        if (name == string.Empty || name.Length > 100)
            throw new DbEntityCreationException(nameof(Plant), nameof(Name));

        if (plantId == string.Empty || plantId.Length > 10)
            throw new DbEntityCreationException(nameof(Plant), nameof(PlantId));

        if (capacityDc < 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(CapacityDc));

        if (capacityAc < 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(CapacityAc));

        if (storageCapacity < 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(StorageCapacity));

        if (projectCompany == string.Empty || projectCompany.Length > 100)
            throw new DbEntityCreationException(nameof(Plant), nameof(ProjectCompany));

        if (utilityCompany == string.Empty || utilityCompany.Length > 100)
            throw new DbEntityCreationException(nameof(Plant), nameof(UtilityCompany));

        if (voltage < 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(Voltage));

        if (assetManager == string.Empty || assetManager.Length > 100)
            throw new DbEntityCreationException(nameof(Plant), nameof(AssetManager));

        if (tags == string.Empty || tags.Length > 150)
            throw new DbEntityCreationException(nameof(Plant), nameof(Tags));

        if (notes == string.Empty || notes.Length > 500)
            throw new DbEntityCreationException(nameof(Plant), nameof(Notes));

        if (plantTypeId <= 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(PlantTypeId));

        if (resourceTypeId <= 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(ResourceTypeId));

        if (statusId <= 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(StatusId));

        if (locationId <= 0)
            throw new DbEntityCreationException(nameof(Plant), nameof(LocationId));
    }
}
