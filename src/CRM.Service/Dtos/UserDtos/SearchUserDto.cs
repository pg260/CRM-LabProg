using CRM.Service.Dtos.PaginatedSearch;
using CRM.Domain.Entities;

namespace CRM.Service.Dtos.UserDtos;

public class SearchUserDto : PaginatedSearchDto<User>
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public bool? Desativado { get; set; }
    
    public override void ApplyFilters(ref IQueryable<User> query)
    {
        if (Nome != null) query = query.Where(c => c.Nome.Contains(Nome));
        if (Email != null) query = query.Where(c => c.Email.Contains(Email));
        if (Desativado != null) query = query.Where(c => c.Desativado == Desativado);
    }

    public override void ApplyOrdenation(ref IQueryable<User> query)
    {
        if (DirectionOfOrdenation.ToLower().Trim().Equals("desc"))
        {
            query = OrdenationBy.ToLower().Trim() switch
            {
                "nome" => query.OrderByDescending(c => c.Nome),
                "email" => query.OrderByDescending(c => c.Email),
                _ => query.OrderByDescending(c => c.CriadoEm)
            };
            return;
        }

        query = OrdenationBy.ToLower().Trim() switch
        {
            "nome" => query.OrderBy(c => c.Nome),
            "email" => query.OrderBy(c => c.Email),
            _ => query.OrderBy(c => c.CriadoEm)
        };
    }
}