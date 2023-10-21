using CRM.Domain.Contracts.PaginatedSearch;
using CRM.Domain.Entities;

namespace CRM.Service.Dtos.PaginatedSearch;

public class PaginatedSearchDto<T> : IPaginatedSearch<T> where T : BaseEntity
{
    public int Pages { get; set; } = 1;
    public int PerPages { get; set; } = 10;
    public string OrdenationBy { get; set; } = "id";
    public string DirectionOfOrdenation { get; set; } = "asc";

    public virtual void ApplyFilters(ref IQueryable<T> query)
    { }

    public virtual void ApplyOrdenation(ref IQueryable<T> query)
    { }
    
}