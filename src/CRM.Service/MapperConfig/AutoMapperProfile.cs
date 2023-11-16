using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Pagination;
using CRM.Service.Dtos.CarrinhoDtos;
using CRM.Service.Dtos.ComprasDto;
using CRM.Service.Dtos.HistoricoComprasDto;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.ProdutoCarrinhoDtos;
using CRM.Service.Dtos.ProdutoDtos;
using CRM.Service.Dtos.UserDtos;

namespace CRM.Service.MapperConfig;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region User

        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<PaginatedResult<User>, PagedDto<UserDto>>().ReverseMap();

        #endregion

        #region Produto

        CreateMap<Produto, AddProdutoDto>().ReverseMap();
        CreateMap<Produto, ProdutoDto>().ReverseMap();
        CreateMap<Produto, EditarProdutoDto>().ReverseMap();
        CreateMap<PaginatedResult<Produto>, PagedDto<ProdutoDto>>().ReverseMap();
        
        #endregion

        #region Carrinho

        CreateMap<Carrinho, CarrinhoDto>().ReverseMap();
        CreateMap<ProdutoCarrinho, AlterarProdutoCarrinhoDto>().ReverseMap();
        CreateMap<ProdutoCarrinho, ProdutoCarrinhoDto>().ReverseMap();

        #endregion

        #region HistoricoCompras


        CreateMap<HistoricoCompras, HistoricoComprasDto>()
            .ForMember(dest => dest.Compras, opt => opt.MapFrom(src => src.Compras))
            .ReverseMap();

        #endregion

        #region Compras

        CreateMap<Compra, CompraDto>().ReverseMap();

        #endregion"
    }
}