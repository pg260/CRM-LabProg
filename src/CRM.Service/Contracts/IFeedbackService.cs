using CRM.Service.Dtos.Feedback;
using CRM.Service.Dtos.PaginatedSearch;

namespace CRM.Service.Contracts;

public interface IFeedbackService
{
    Task Criar(AddFeedbackDto dto);
    Task Excluir(int id);
    Task<FeedbackDto?> ObterPorId(int id);
    Task<PagedDto<FeedbackDto>> Buscar(BuscarFeedbackDto dto);
}