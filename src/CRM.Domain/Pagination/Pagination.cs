using CRM.Domain.Contracts.PaginatedSearch;

namespace CRM.Domain.Pagination;

public class Pagination : IPagination
{
    public int TotalItems { get; set; }
    public int TotalInPage { get; set; }
    public int PageNumber { get; set; }
    public int CapacityItems { get; set; }
    public int TotalPages { get; set; }
}