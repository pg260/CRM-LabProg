using CRM.Service.Dtos.CarrinhoDtos;
using CRM.Service.Dtos.ProdutoCarrinhoDtos;

namespace CRM.Service.Contracts;

public interface ICarrinhoService
{
    Task<CarrinhoDto?> ObterPorId(int id);
    Task AdicionarProduto(int carrinhoId, AlterarProdutoCarrinhoDto dto);
    Task RemoverProduto(int carrinhoId, AlterarProdutoCarrinhoDto dto);
}