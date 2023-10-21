namespace CRM.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}