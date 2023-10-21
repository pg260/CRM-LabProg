using CRM.Domain.Contracts.PaginatedSearch;

namespace CRM.Service.Dtos.PaginatedSearch;

public class PagedDto<T> : IPaginatedResult<T>
{
    public PagedDto()
    {
        Items = new List<T>();
        Pagination = new IPagination();
    }

    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}