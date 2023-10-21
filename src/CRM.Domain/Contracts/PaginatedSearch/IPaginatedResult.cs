namespace CRM.Domain.Contracts.PaginatedSearch;

public interface IPaginatedResult<T>
{
    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}