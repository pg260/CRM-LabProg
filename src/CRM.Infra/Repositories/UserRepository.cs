using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(BaseDbContext context) : base(context)
    { }

    public void Criar(User user) => Context.Users.Add(user);

    public void Editar(User user) => Context.Users.Update(user);

    public void Remover(User user) => Context.Users.Remove(user);

    public async Task<User?> ObterPorId(int id)
    {
        return await Context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}