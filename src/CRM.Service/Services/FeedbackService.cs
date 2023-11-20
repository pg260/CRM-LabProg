using AutoMapper;
using CRM.Core.Authorization;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.Feedback;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.NotificatorConfig;

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
        var feedback = Mapper.Map<Feedback>(dto);

        var dic = await Validar(feedback);
        if (dic == null) return;

        _feedbackRepository.Criar(dic.Keys.First());
        _produtoRepository.Editar(dic.Values.First());

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

    private async Task<Dictionary<Feedback, Produto>?> Validar(Feedback feedback)
    {
        if (!feedback.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return null;  
        }
        
        var compra = await _comprasRepository.FirstOrDefault(c =>
            c.UserId == _authenticatedUser.Id && c.ProdutoId == feedback.ProdutoId);
        if (compra == null)
        {
            Notificator.Handle("Você não realizou uma compra para este produto.");
            return null;
        }
        
        var produto = await _produtoRepository.FirstOrDefault(c => c.Id == compra.ProdutoId && !c.Desativado);
        if (produto == null)
        {
            Notificator.Handle("Este produto não existe mais.");
            return null;
        }
        
        feedback.CompraId = compra.Id;
        feedback.UserId = _authenticatedUser.Id;

        if (await _feedbackRepository.Any(c =>
                c.ProdutoId == feedback.ProdutoId && feedback.UserId == _authenticatedUser.Id))
        {
            Notificator.Handle("Você já avaliou esse produto.");
            return null;
        }

        produto.Nota = await _feedbackRepository.MediaAvaliacoes(produto.Id, feedback.Avaliacao);

        var dicionario = new Dictionary<Feedback, Produto>();
        dicionario.Add(feedback, produto);
        return dicionario;
    }
}