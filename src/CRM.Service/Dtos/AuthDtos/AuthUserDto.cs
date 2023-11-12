using CRM.Service.Dtos.UserDtos;

namespace CRM.Service.Dtos.AuthDtos;

public class AuthUserDto
{
    public UserDto UserDto { get; set; } = null!;
    public string Token { get; set; } = null!;
}