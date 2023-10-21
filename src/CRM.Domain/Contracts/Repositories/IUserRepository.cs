using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    void Criar(User user);
    void Editar(User user);
    void Remover(User user);
    Task<User?> ObterPorId(int id);
}