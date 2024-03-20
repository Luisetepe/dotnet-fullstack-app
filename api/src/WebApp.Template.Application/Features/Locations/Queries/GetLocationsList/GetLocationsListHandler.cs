using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.DbEntities;
using WebApp.Template.Application.Data.Services;
using WebApp.Template.Application.Shared.Models;

namespace WebApp.Template.Application.Features.Locations.Queries.GetLocationsList;

public class GetLocationsListHandler(WebAppDbContext db, IUniqueIdentifierService identifierService) : IRequestHandler<GetLocationsListQuery, GetLocationsListResponse>
{
    public async Task<GetLocationsListResponse> Handle(GetLocationsListQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var filteredQuery = BuildFilteredQuery(query);

            var locations = await filteredQuery
                .Skip((query.Request.PageNumber - 1) * query.Request.PageSize)
                .Take(query.Request.PageSize)
                .Select(x => new GetLocationsListResponseDto.Location
                {
                    Id = identifierService.ConvertToString(x.Id),
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
            return new GetLocationsListResponse(new GetLocationsListResponseDto
            {
                Locations = locations,
                Pagination = paginationInfo
            });
        }
        catch (Exception ex)
        {
            return new GetLocationsListResponse(ex.Message);
        }
    }


    private IQueryable<Location> BuildFilteredQuery(GetLocationsListQuery query)
    {
        Expression<Func<Location, object>> orderExpression = x => x.Name;

        var filteredQuery = db.Locations
            .AsNoTracking()
            .Where(x => string.IsNullOrWhiteSpace(query.Request.Search) || x.Name.Contains(query.Request.Search));

        if (!string.IsNullOrWhiteSpace(query.Request.OrderBy))
        {
            orderExpression = query.Request.OrderBy.ToLower() switch
            {
                "name" => x => x.Name,
                "latitude" => x => x.Latitude,
                "longitude" => x => x.Longitude,
                _ => throw new ArgumentOutOfRangeException(nameof(query.Request.OrderBy), query.Request.OrderBy)
            };
        }

        filteredQuery = query.Request.Order == "asc"
                ? filteredQuery.OrderBy(orderExpression).ThenBy(x => x.Id)
                : filteredQuery.OrderByDescending(orderExpression).ThenByDescending(x => x.Id);

        return filteredQuery;
    }
}