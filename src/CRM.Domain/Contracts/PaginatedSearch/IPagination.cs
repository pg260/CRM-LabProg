namespace CRM.Domain.Contracts.PaginatedSearch;

public class IPagination
{
    public int TotalItems { get; set; }
    public int TotalInPage { get; set; }
    public int PageNumber { get; set; }
    public int CapacityItems { get; set; }
    public int TotalPages { get; set; }
}