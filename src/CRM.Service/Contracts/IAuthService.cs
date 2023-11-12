using CRM.Service.Dtos.AuthDtos;

namespace CRM.Service.Contracts;

public interface IAuthService
{
    Task<AuthUserDto?> Login(LoginDto dto);
}