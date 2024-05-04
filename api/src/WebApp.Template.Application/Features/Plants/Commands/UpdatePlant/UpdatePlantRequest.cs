using Ardalis.Result;
using FastEndpoints;
using FluentValidation;
using MediatR;

namespace WebApp.Template.Application.Features.Plants.Commands.UpdatePlant;

public class UpdatePlantCommand : IRequest<Result>
{
    public UpdatePlantRequest Request { get; set; }
}

public record UpdatePlantRequest
{
    public string Id { get; set; }
    public double CapacityDc { get; init; }
    public double CapacityAc { get; init; }
    public double StorageCapacity { get; init; }
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

public class UpdatePlantRequestValidator : Validator<UpdatePlantRequest>
{
    public UpdatePlantRequestValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("The 'id' field is required.");
        RuleFor(p => p.CapacityDc)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The 'capacityDc' field must be greater than or equal to 0.");
        RuleFor(p => p.CapacityAc)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The 'capacityAc' field must be greater than or equal to 0.");
        RuleFor(p => p.StorageCapacity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The 'storageCapacity' field must be greater than or equal to 0.");
        RuleFor(p => p.ProjectCompany)
            .NotEmpty()
            .WithMessage("The 'projectCompany' field is required.")
            .MaximumLength(100)
            .WithMessage(
                "The 'projectCompany' field must be less than or equal to 100 characters."
            );
        RuleFor(p => p.UtilityCompany)
            .NotEmpty()
            .WithMessage("The 'utilityCompany' field is required.")
            .MaximumLength(100)
            .WithMessage(
                "The 'utilityCompany' field must be less than or equal to 100 characters."
            );
        RuleFor(p => p.Voltage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The 'voltage' field must be greater than or equal to 0.");
        RuleFor(p => p.AssetManager)
            .NotEmpty()
            .WithMessage("The 'assetManager' field is required.")
            .MaximumLength(100)
            .WithMessage("The 'assetManager' field must be less than or equal to 100 characters.");
        RuleFor(p => p.Tags)
            .NotEmpty()
            .WithMessage("The 'tags' field is required.")
            .MaximumLength(150)
            .WithMessage("The 'tags' field must be less than or equal to 150 characters.");
        RuleFor(p => p.Notes)
            .MaximumLength(500)
            .When(p => p.Notes != null)
            .WithMessage("The 'notes' field must be less than or equal to 500 characters.");
        RuleFor(p => p.PlantTypeId).NotEmpty().WithMessage("The 'plantTypeId' field is required.");
        RuleFor(p => p.ResourceTypeId)
            .NotEmpty()
            .WithMessage("The 'resourceTypeId' field is required.");
        RuleFor(p => p.StatusId).NotEmpty().WithMessage("The 'statusId' field is required.");
        RuleFor(p => p.LocationId).NotEmpty().WithMessage("The 'locationId' field is required.");
    }
}
