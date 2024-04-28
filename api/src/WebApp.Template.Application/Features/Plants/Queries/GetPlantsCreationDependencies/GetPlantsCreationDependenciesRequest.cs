using Ardalis.Result;
using MediatR;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsCreationDependencies;

public class GetPlantsCreationDependenciesQuery : IRequest<Result<GetPlantsCreationDependenciesResponse>> { }
