using AutoMapper;
using CRM.Core.Authorization;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.Feedback;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.NotificatorConfig;
using FluentValidation.Results;

namespace CRM.Service.Services;

public class FeedbackService : BaseService, IFeedbackService
{
    public FeedbackService(IMapper mapper, INotificator notificator, IFeedbackRepository feedbackRepository,
        IComprasRepository comprasRepository, IAuthenticatedUser authenticatedUser, IProdutoRepository produtoRepository) : base(mapper, notificator)
    {
        _feedbackRepository = feedbackRepository;
        _comprasRepository = comprasRepository;
        _authenticatedUser = authenticatedUser;
        _produtoRepository = produtoRepository;
    }

    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IComprasRepository _comprasRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IAuthenticatedUser _authenticatedUser;

    public async Task Criar(AddFeedbackDto dto)
    {
        var compra = await _comprasRepository.FirstOrDefault(c =>
            c.UserId == _authenticatedUser.Id && c.ProdutoId == dto.ProdutoId);

        if (compra == null)
        {
            Notificator.Handle("Você não realizou uma compra para este produto.");
            return;
        }
        
        var produto = await _produtoRepository.FirstOrDefault(c => c.Id == compra.ProdutoId && !c.Desativado);
        if (produto == null)
        {
            Notificator.Handle("Este produto não existe mais.");
            return;
        }

        var feedback = Mapper.Map<Feedback>(dto);
        feedback.CompraId = compra.Id;
        feedback.UserId = _authenticatedUser.Id;
        feedback.ProdutoId = produto.Id;

        if (!Validar(feedback)) return;

        _feedbackRepository.Criar(feedback);

        produto.Nota = await _feedbackRepository.MediaAvaliacoes(produto.Id, feedback.Avaliacao);
        _produtoRepository.Editar(produto);

        if (!await Commit()) Notificator.Handle("Ocorreu um erro ao salvar o feedback.");
    }

    public async Task Excluir(int id)
    {
        var feedback = await _feedbackRepository.FirstOrDefault(c => c.Id == id && c.UserId == _authenticatedUser.Id);
        if(feedback == null)
        {
            Notificator.Handle("Só é permitido excluir os seus proprios feedbacks.");
            return;
        }
        
        _feedbackRepository.Excluir(feedback);
        if (!await Commit()) Notificator.Handle("Não foi possível excluir o feedback.");
    }

    public async Task<FeedbackDto?> ObterPorId(int id)
    {
        var feedback = await _feedbackRepository.ObterPorId(id);
        if (feedback != null) return Mapper.Map<FeedbackDto>(feedback);
        
        Notificator.HandleNotFound();
        return null;
    }

    public async Task<PagedDto<FeedbackDto>> Buscar(BuscarFeedbackDto dto)
    {
        var feedback = await _feedbackRepository.Search(dto);
        return Mapper.Map<PagedDto<FeedbackDto>>(feedback);
    }

    private async Task<bool> Commit() => await _feedbackRepository.UnitOfWork.Commit();

    private bool Validar(Feedback feedback)
    {
        if (feedback.Validar(out ValidationResult validationResult)) return true;
        Notificator.Handle(validationResult.Errors);
        return false;
    }
}