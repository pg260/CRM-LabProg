using AutoMapper;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.UserDtos;
using CRM.Service.NotificatorConfig;

namespace CRM.Service.Services;

public class UserService : BaseService, IUserService
{
    public UserService(IMapper mapper, INotificator notificator, IUserRepository userRepository) : base(mapper,
        notificator)
    {
        _userRepository = userRepository;
    }

    private readonly IUserRepository _userRepository;

    public async Task Criar(CreateUserDto dto)
    {
        var user = Mapper.Map<User>(dto);
        if (!await Validar(user)) return;

        user.Carrinho = new Carrinho
            { UserId = user.Id, ValorTotal = 0 };

        _userRepository.Criar(user);

        if (!await CommitChanges()) Notificator.Handle("Não foi possível salvar o usuário no banco de dados.");
    }

    public async Task Editar(int id, UpdateUserDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("Os ids não conferem.");
            return;
        }

        var user = Mapper.Map<User>(dto);
        if (!await Validar(user)) return;

        _userRepository.Editar(user);

        if (!await CommitChanges())
            Notificator.Handle("Não foi possível salvar as alterações de usuário no banco de dados.");
    }

    public async Task Remover(int id)
    {
        var user = await _userRepository.FirstOrDefault(c => c.Id == id);
        if (user == null)
        {
            Notificator.HandleNotFound();
            return;
        }

        _userRepository.Remover(user);

        if (!await CommitChanges())
            Notificator.Handle("Não foi possível salvar as alterações de usuário no banco de dados.");
    }

    public async Task<UserDto?> ObterPorId(int id)
    {
        var user = await _userRepository.ObterPorId(id);
        if (user != null) return Mapper.Map<UserDto>(user);

        Notificator.HandleNotFound();
        return null;
    }

    public async Task<PagedDto<UserDto>> Pesquisar(SearchUserDto dto)
    {
        var users = await _userRepository.Search(dto);
        return Mapper.Map<PagedDto<UserDto>>(users);
    }

    private async Task<bool> Validar(User user)
    {
        if (!user.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return false;
        }

        if (!await _userRepository.Any(c => c.Id == user.Id) && user.Id != 0)
        {
            Notificator.HandleNotFound();
            return false;
        }

        if (!await _userRepository.Any(c => c.Email == user.Email && c.Id != user.Id)) return true;

        Notificator.Handle("Esse email já está em uso.");
        return false;
    }

    private async Task<bool> CommitChanges() => await _userRepository.UnitOfWork.Commit();
}