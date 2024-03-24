using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Data.Services;
using WebApp.Template.Application.Shared.Exceptions;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Plants.Queries.GetPlantsList;

public class GetPlantsListHandler(WebAppDbContext db, IUniqueIdentifierService identifierService)
    : IRequestHandler<GetPlantsListQuery, GetPlantsListResponse>
{
    public async Task<GetPlantsListResponse> Handle(
        GetPlantsListQuery query,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var filteredQuery = BuildFilteredQuery(query);

            var plants = await filteredQuery
                .Skip((query.Request.PageNumber - 1) * query.Request.PageSize)
                .Take(query.Request.PageSize)
                .Select(x => new GetPlantsListResponseDto.Plant
                {
                    Id = identifierService.ConvertToString(x.Id),
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

            return new GetPlantsListResponse(
                new GetPlantsListResponseDto { Plants = plants, Pagination = paginationInfo }
            );
        }
        catch (SearcException ex)
        {
            return new GetPlantsListResponse(ex.Message, StatusCode.ValidationError);
        }
        catch (Exception ex)
        {
            return new GetPlantsListResponse(ex.Message, StatusCode.UnhandledError);
        }
    }

    private IQueryable<Plant> BuildFilteredQuery(GetPlantsListQuery query)
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
            orderExpression = query.Request.OrderBy.ToLower() switch
            {
                "name" => x => x.Name,
                "status" => x => x.Status.Name,
                _ => throw new OrderByException(query.Request.OrderBy)
            };
        }

        filteredQuery =
            query.Request.Order == "asc"
                ? filteredQuery.OrderBy(orderExpression).ThenBy(x => x.Id)
                : filteredQuery.OrderByDescending(orderExpression).ThenByDescending(x => x.Id);

        return filteredQuery;
    }
}
