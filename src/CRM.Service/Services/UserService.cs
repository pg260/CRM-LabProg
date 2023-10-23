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
    public UserService(IMapper mapper, INotificator notificator, IUserRepository userRepository,
        IHashService hashService) : base(mapper,
        notificator)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;

    public async Task Criar(CreateUserDto dto)
    {
        var user = new User();
        if (dto.Foto64 != null) user.Foto = TransformandoImagemParaSalvar(dto.Foto64);

        Mapper.Map(dto, user);
        user = PreenchendoUsuario(user);
        if (!await Validar(user)) return;

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

        var userExistente = await _userRepository.FirstOrDefault(c => c.Id == dto.Id);
        if (userExistente == null)
        {
            Notificator.HandleNotFound();
            return;
        }

        Mapper.Map(dto, userExistente);
        userExistente.AtualizadoEm = DateTime.Now;
        if (!await Validar(userExistente)) return;

        _userRepository.Editar(userExistente);

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
        if (user == null)
        {
            Notificator.HandleNotFound();
            return null;   
        }

        var userDto = Mapper.Map<UserDto>(user);
        if (user.Foto != null) userDto.Foto = TransformandoImagemParaDevolver(user.Foto);

        return userDto;
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

        if (!await _userRepository.Any(c => c.Email == user.Email && c.Id != user.Id)) return true;

        Notificator.Handle("Esse email já está em uso.");
        return false;
    }

    private async Task<bool> CommitChanges() => await _userRepository.UnitOfWork.Commit();

    private User PreenchendoUsuario(User user)
    {
        user.Carrinho = new Carrinho { ValorTotal = 0 };
        user.Senha = _hashService.GerarHash(user.Senha);

        return user;
    }

    private byte[] TransformandoImagemParaSalvar(string imagem) => Convert.FromBase64String(imagem);

    private string TransformandoImagemParaDevolver(byte[] imagem) => Convert.ToBase64String(imagem);
}