using Ardalis.Result;
using FastEndpoints;
using FluentValidation;
using MediatR;

namespace WebApp.Template.Application.Features.Plants.Commands.CreatePlant;

public class CreatePlantCommand : IRequest<Result<CreatePlantResponse>>
{
    public CreatePlantRequest Request { get; set; }
}

public record CreatePlantRequest
{
    public string Name { get; init; }
    public string PlantId { get; init; }
    public decimal CapacityDc { get; init; }
    public decimal CapacityAc { get; init; }
    public decimal StorageCapacity { get; init; }
    public string ProjectCompany { get; init; }
    public string UtilityCompany { get; init; }
    public int Voltage { get; init; }
    public string AssetManager { get; init; }
    public string Tags { get; init; }
    public string Notes { get; init; }
    public string PlantTypeId { get; init; }
    public string ResourceTypeId { get; init; }
    public string StatusId { get; init; }
    public string LocationId { get; init; }
    public IReadOnlyCollection<string> PortfolioIds { get; init; }
}

public class CreatePlantRequestValidator : Validator<CreatePlantRequest>
{
    public CreatePlantRequestValidator()
    {
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
        RuleFor(p => p.PlantTypeId).NotEmpty();
        RuleFor(p => p.ResourceTypeId).NotEmpty();
        RuleFor(p => p.StatusId).NotEmpty();
        RuleFor(p => p.LocationId).NotEmpty();
    }
}
