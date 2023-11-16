using CRM.Service.Dtos.HistoricoComprasDto;

namespace CRM.Service.Contracts;

public interface IHistoricoComprasService
{
    Task Comprando(ComprandoDto dto);
    Task<HistoricoComprasDto?> ObterPorId(int id);
}