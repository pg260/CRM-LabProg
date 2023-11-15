using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.ProdutoDtos;

namespace CRM.Service.Contracts
{
    public interface IProdutoService
    {
        Task<ProdutoDto?> Criar(AddProdutoDto dto);
        Task Editar(int id, EditarProdutoDto dto);
        Task Ativar(int id);
        Task Desativar(int id);
        Task<ProdutoDto?> ObterPorId(int id);
        Task<PagedDto<ProdutoDto>> Pesquisar(BuscarProdutoDto dto);
    }
}