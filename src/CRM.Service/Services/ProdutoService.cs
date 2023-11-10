using AutoMapper;
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
        
        public ProdutoService(IMapper mapper, INotificator notificator, IProdutoRepository produtoRepository) : base(mapper, notificator)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoDto?> Criar(AddProdutoDto dto)
        {
            var produto = Mapper.Map<Produto>(dto);
            if (!await Validar(produto)) 
                return null;
            
            _produtoRepository.Criar(produto);
            if (await CommitChanges()) 
                return Mapper.Map<ProdutoDto>(produto);
           
            Notificator.Handle("Não foi possível cadastrar o Produto.");
            return null;
        }

        public async Task<ProdutoDto?> Editar(int id, ProdutoDto dto)
        {
            if (id != dto.Id)
            {
                Notificator.HandleNotFound();
                return null;
            }

            var produto = await _produtoRepository.ObterPorId(dto.Id);
            if(produto == null)
            {
                Notificator.HandleNotFound();
                return null;
            }

            if (produto.Desativado)
            {
                Notificator.Handle("O produto está desativado");
                return null;
            }

            Mapper.Map(dto, produto);
            if (!await Validar(produto)) return null;
            
            _produtoRepository.Editar(produto);
            if (await CommitChanges())
                return Mapper.Map<ProdutoDto>(produto);
            
            Notificator.Handle("Não foi possível editar o Produto.");
            return null;
        }

        public async Task Ativar(int id)
        {
            var produto = await _produtoRepository.FirstOrDefault(p => p.Id == id);
            if (produto == null)
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
            if (produto == null)
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
        
        private async Task<bool> Validar(Produto produto)
        {
            if (!produto.Validar(out ValidationResult validationResult))
            {
                Notificator.Handle(validationResult.Errors);
                return false;
            }

            return true;
        }
        
        private async Task<bool> CommitChanges() => await _produtoRepository.UnitOfWork.Commit();
    }
}