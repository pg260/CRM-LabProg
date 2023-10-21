using CRM.Domain.Contracts.PaginatedSearch;

namespace CRM.Domain.Pagination;

public class PaginatedResult<T> : IPaginatedResult<T>
{
    public PaginatedResult()
    {
        Pagination = new IPagination();
        Items = new List<T>();
    }

    public PaginatedResult(int page, int total, int perPages) : this()
    {
        Pagination = new IPagination()
        {
            PageNumber = page,
            TotalPages = total,
            TotalItems = total,
            TotalInPage = perPages
        };
    }
    
    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}