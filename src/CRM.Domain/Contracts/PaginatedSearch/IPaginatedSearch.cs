using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.PaginatedSearch;

public interface IPaginatedSearch<T> where T : BaseEntity
{
    public int Pages { get; set; }
    public int PerPages { get; set; }
    public string OrdenationBy { get; set; }
    public string DirectionOfOrdenation { get; set; }

    public void ApplyFilters(ref IQueryable<T> query);
    public void ApplyOrdenation(ref IQueryable<T> query);
}