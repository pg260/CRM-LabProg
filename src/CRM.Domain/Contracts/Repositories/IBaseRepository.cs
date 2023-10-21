using System.Linq.Expressions;
using CRM.Domain.Contracts.PaginatedSearch;
using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    public IUnitOfWork UnitOfWork { get; }
    Task<IPaginatedResult<T>> Search(IPaginatedSearch<T> filtro);
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
    Task<bool> Any(Expression<Func<T, bool>> predicate);
}