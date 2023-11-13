using AutoMapper;
using CRM.Core.Authorization;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.ProdutoDtos;
using CRM.Service.NotificatorConfig;
using FluentValidation.Results;

namespace CRM.Service.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
         private readonly IProdutoRepository _produtoRepository;
         private readonly IAuthenticatedUser _authenticatedUser;
        
        public ProdutoService(IMapper mapper, INotificator notificator, IProdutoRepository produtoRepository,
            IAuthenticatedUser authenticatedUser) : base(mapper, notificator)
        {
            _produtoRepository = produtoRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<ProdutoDto?> Criar(AddProdutoDto dto)
        {
            var produto = Mapper.Map<Produto>(dto);
            produto.UserId = _authenticatedUser.Id;
            if (!Validar(produto)) 
                return null;
            
            _produtoRepository.Criar(produto);
            if (await CommitChanges()) 
                return Mapper.Map<ProdutoDto>(produto);
           
            Notificator.Handle("Não foi possível cadastrar o Produto.");
            return null;
        }

        public async Task Editar(int id, ProdutoDto dto)
        {
            if (id != dto.Id)
            {
                Notificator.Handle("Os ids informados não conferem.");
                return;
            }
            
            dto.CriadoEm = DateTime.Now;
            var produto = await _produtoRepository.FirstOrDefault(p => p.Id == dto.Id);
            if(produto == null || produto.UserId != _authenticatedUser.Id)
            {
                Notificator.HandleNotFound();
                return;
            }
            
            if (produto.Desativado)
            {
                Notificator.Handle("O produto está desativado.");
                return;
            }

            Mapper.Map(dto, produto);
            if (!Validar(produto))
            {
                return;
            }
            
            _produtoRepository.Editar(produto);
            if (await CommitChanges())
            {
                Mapper.Map<ProdutoDto>(produto);
            }

            Notificator.Handle("Não foi possível editar o Produto.");
        }

        public async Task Ativar(int id)
        {
            var produto = await _produtoRepository.FirstOrDefault(p => p.Id == id);
            if (produto == null || produto.UserId != _authenticatedUser.Id)
            {
                Notificator.HandleNotFound();
                return;
            }

            if (!produto.Desativado)
            {
                Notificator.Handle("Esse produto já está ativo.");
                return;
            }

            produto.Desativado = false;
            _produtoRepository.Editar(produto);

            if (!await CommitChanges())
            {
                Notificator.Handle("Não foi possível salvar no banco.");
            }
        }

        public async Task Desativar(int id)
        {
            var produto = await _produtoRepository.FirstOrDefault(p => p.Id == id);
            if (produto == null || produto.UserId != _authenticatedUser.Id)
            {
                Notificator.HandleNotFound();
                return;
            }

            if (produto.Desativado)
            {
                Notificator.Handle("Esse produto está desativado.");
                return;
            }

            produto.Desativado = true;
            _produtoRepository.Editar(produto);
            if (!await CommitChanges())
            {
                Notificator.Handle("Não foi possível salvar no banco.");
            }
        }

        public async Task<ProdutoDto?> ObterPorId(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto != null)
                return Mapper.Map<ProdutoDto>(produto);
            
            Notificator.HandleNotFound();
            return null;
        }

        public async Task<PagedDto<ProdutoDto>> Pesquisar(BuscarProdutoDto dto)
        {
            var produto = await _produtoRepository.Search(dto);
            return Mapper.Map<PagedDto<ProdutoDto>>(produto);
        }
        
        private bool Validar(Produto produto)
        {
            if (produto.Validar(out ValidationResult validationResult)) return true;
            Notificator.Handle(validationResult.Errors);
            return false;
        }
        
        private async Task<bool> CommitChanges() => await _produtoRepository.UnitOfWork.Commit();
    }
}