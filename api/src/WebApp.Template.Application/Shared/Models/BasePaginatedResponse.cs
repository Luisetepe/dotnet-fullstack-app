namespace WebApp.Template.Application.Shared.Models;

public record PaginationInfo
{
    public int CurrentPageNumber { get; init; }
    public int CurrentPageSize { get; init; }
    public int TotalRows { get; init; }
    public int TotalPages { get; init; }
}

public record BasePaginatedResponse
{
    public PaginationInfo Pagination { get; init; }
}
