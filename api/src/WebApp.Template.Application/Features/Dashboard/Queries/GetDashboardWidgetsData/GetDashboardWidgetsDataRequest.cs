using Ardalis.Result;
using MediatR;

namespace WebApp.Template.Application.Features.Dashboard.Queries.GetDashboardWidgetsData;

public class GetDashboardWidgetsDataQuery : IRequest<Result<GetDashboardWidgetsDataResponse>> { }
