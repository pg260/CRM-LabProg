using CRM.Service.Dtos.HistoricoComprasDto;
using CRM.Service.Dtos.PaginatedSearch;

namespace CRM.Service.Contracts;

public interface IHistoricoComprasService
{
    Task Comprando(ComprandoDto dto);
    Task<HistoricoComprasDto?> ObterPorId(int id);
    Task<PagedDto<HistoricoComprasDto>> Buscar(BuscarHistoricoComprasDto dto);
}