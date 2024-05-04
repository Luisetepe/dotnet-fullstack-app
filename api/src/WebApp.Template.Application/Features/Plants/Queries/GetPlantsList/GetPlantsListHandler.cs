using System.Linq.Expressions;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;

public class GetPlantsListHandler(WebAppDbContext db)
    : IRequestHandler<GetPlantsListQuery, Result<GetPlantsListResponse>>
{
    public async Task<Result<GetPlantsListResponse>> Handle(
        GetPlantsListQuery query,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var filterQueryResult = BuildFilteredQuery(query);

            if (!filterQueryResult.IsSuccess)
            {
                return Result.Invalid(filterQueryResult.ValidationErrors.ToArray());
            }

            var filteredQuery = filterQueryResult.Value;
            var plants = await filteredQuery
                .Skip((query.Request.PageNumber - 1) * query.Request.PageSize)
                .Take(query.Request.PageSize)
                .Select(x => new GetPlantsListResponse.Plant
                {
                    Id = x.Id,
                    Name = x.Name,
                    PlantId = x.PlantId,
                    UtilityCompany = x.UtilityCompany,
                    Status = x.Status.Name,
                    Tags = x.Tags.Split(
                        ',',
                        StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries
                    ),
                    CapacityDc = x.CapacityDc,
                    Portfolios = x.Portfolios.Select(y => y.Name).ToArray()
                })
                .ToArrayAsync(cancellationToken);

            var totalRows = await filteredQuery.CountAsync(cancellationToken);
            var paginationInfo = new PaginationInfo
            {
                CurrentPageNumber = query.Request.PageNumber,
                CurrentPageSize = query.Request.PageSize,
                TotalRows = totalRows,
                TotalPages = (int)Math.Ceiling((double)totalRows / query.Request.PageSize)
            };

            return new GetPlantsListResponse { Plants = plants, Pagination = paginationInfo };
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }

    private Result<IQueryable<Plant>> BuildFilteredQuery(GetPlantsListQuery query)
    {
        Expression<Func<Plant, object>> orderExpression = x => x.Name;

        var filteredQuery = db
            .Plants.Include(x => x.Status)
            .Include(x => x.Portfolios)
            .AsNoTracking()
            .Where(x =>
                string.IsNullOrWhiteSpace(query.Request.Search)
                || x.Name.Contains(query.Request.Search)
            );

        if (!string.IsNullOrWhiteSpace(query.Request.OrderBy))
        {
            switch (query.Request.OrderBy.ToLower())
            {
                case "name":
                    orderExpression = x => x.Name;
                    break;
                case "status":
                    orderExpression = x => x.Status.Name;
                    break;
                default:
                    return Result.Invalid(
                        new ValidationError
                        {
                            Identifier = nameof(query.Request.OrderBy),
                            ErrorMessage = $"Invalid OrderBy value: {query.Request.OrderBy}"
                        }
                    );
            }
        }

        filteredQuery =
            query.Request.Order == "asc"
                ? filteredQuery.OrderBy(orderExpression).ThenBy(x => x.Id)
                : filteredQuery.OrderByDescending(orderExpression).ThenByDescending(x => x.Id);

        return Result.Success(filteredQuery);
    }
}
