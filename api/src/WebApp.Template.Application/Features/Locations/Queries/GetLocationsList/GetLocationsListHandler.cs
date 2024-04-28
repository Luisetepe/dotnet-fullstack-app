using System.Linq.Expressions;
using Ardalis.Result;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;

public class GetLocationsListHandler(WebAppDbContext db)
    : IRequestHandler<GetLocationsListQuery, Result<GetLocationsListResponse>>
{
    public async Task<Result<GetLocationsListResponse>> Handle(
        GetLocationsListQuery query,
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
            var locations = await filteredQuery
                .Skip((query.Request.PageNumber - 1) * query.Request.PageSize)
                .Take(query.Request.PageSize)
                .Select(x => new GetLocationsListResponse.Location
                {
                    Id = x.Id,
                    Name = x.Name,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
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

            return new GetLocationsListResponse { Locations = locations, Pagination = paginationInfo };
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }

    private Result<IQueryable<Location>> BuildFilteredQuery(GetLocationsListQuery query)
    {
        Expression<Func<Location, object>> orderExpression = x => x.Name;

        var filteredQuery = db
            .Locations.AsNoTracking()
            .Where(x => string.IsNullOrWhiteSpace(query.Request.Search) || x.Name.Contains(query.Request.Search));

        if (!string.IsNullOrWhiteSpace(query.Request.OrderBy))
        {
            switch (query.Request.OrderBy.ToLower())
            {
                case "name":
                    orderExpression = x => x.Name;
                    break;
                case "latitude":
                    orderExpression = x => x.Latitude;
                    break;
                case "longitude":
                    orderExpression = x => x.Longitude;
                    break;
                default:
                    return Result.Invalid(
                        new ValidationError
                        {
                            Identifier = nameof(query.Request.OrderBy),
                            ErrorMessage = $"Invalid order by field: {query.Request.OrderBy}"
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
