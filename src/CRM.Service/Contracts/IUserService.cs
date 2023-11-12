using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.UserDtos;

namespace CRM.Service.Contracts;

public interface IUserService
{
    Task Criar(CreateUserDto dto);
    Task Editar(int id, UpdateUserDto dto);
    Task Desativar(int id);
    Task Ativar(int id);
    Task<UserDto?> ObterPorId(int id);
    Task<PagedDto<UserDto>> Pesquisar(SearchUserDto dto);
}