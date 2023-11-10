using CRM.Domain.Entities;
using CRM.Service.Dtos.PaginatedSearch;

namespace CRM.Service.Dtos.ProdutoDtos
{
    public class BuscarProdutoDto : PaginatedSearchDto<Produto>
    {
        public int UserId { get; set; }
        public string? Nome { get; set; }
        public float? Valor { get; set; }
        public string? Descricao { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        
        public override void ApplyFilters(ref IQueryable<Produto> query)
        {
            query = query.Where(p => p.UserId == UserId);
            if (Nome != null) 
                query = query.Where(p => p.Nome.ToLower().Contains(Nome.Trim().ToLower()));
            if (Valor > 0) 
                query = query.Where(p => p.Valor > Valor);
            if (Descricao != null) 
                query = query.Where(p => p.Descricao.ToLower().Contains(Descricao.Trim().ToLower()));
            if (Cidade != null)
                query = query.Where(p => p.Cidade.ToLower().Contains(Cidade.Trim().ToLower()));
            if (Estado != null)
                query = query.Where(p => p.Estado.ToLower().Contains(Estado.Trim().ToLower()));
        }
        
        public override void ApplyOrdenation(ref IQueryable<Produto> query)
        {
            if (DirectionOfOrdenation.Trim().ToLower().Equals("desc"))
            {
                query = OrdenationBy.ToLower().Trim() switch
                {
                    "nome" => query.OrderByDescending(p => p.Nome),
                    "valor" => query.OrderByDescending(p => p.Valor),
                    "descricao" => query.OrderByDescending(p => p.Descricao),
                    "cidade" => query.OrderByDescending(p => p.Cidade),
                    "estado" => query.OrderByDescending(p => p.Estado),
                    _ => query.OrderByDescending(p => p.Id)
                };
            }

            query = OrdenationBy.ToLower().Trim() switch
            {
                "nome" => query.OrderBy(p => p.Nome),
                "valor" => query.OrderBy(p => p.Valor),
                "descricao" => query.OrderBy(p => p.Descricao),
                "cidade" => query.OrderBy(p => p.Cidade),
                "estado" => query.OrderBy(p => p.Estado),
                _ => query.OrderBy(p => p.Id)
            };
        }
    }
}