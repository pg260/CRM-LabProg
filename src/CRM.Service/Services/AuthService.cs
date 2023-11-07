using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CRM.Core.JwtSettings;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.AuthDtos;
using CRM.Service.Dtos.UserDtos;
using CRM.Service.NotificatorConfig;
using Microsoft.IdentityModel.Tokens;

namespace CRM.Service.Services;

public class AuthService : BaseService, IAuthService
{
    public AuthService(IMapper mapper, INotificator notificator, IUserRepository userRepository, IHashService hashService) : base(mapper, notificator)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }
    
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    
    public async Task<AuthUserDto?> Login(LoginDto dto)
    {
        var user = await _userRepository.FirstOrDefault(c => c.Email == dto.Email);
        if (user == null)
        {
            Notificator.Handle("Email incorreto.");
            return null;
        }

        if (!_hashService.VerificarHash(dto.Senha, user.Senha))
        {
            Notificator.Handle("Senha incorreta.");
            return null;
        }

        var token = GenerateToken(user, Settings.Secret);
        var userDto = Mapper.Map<UserDto>(user);
        
        return new AuthUserDto
        {
            UserDto = userDto,
            Token = token
        };
    }
    
    private string GenerateToken(User users, string key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var encodeKey = Encoding.ASCII.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                new Claim(ClaimTypes.Name, users.Nome),
                new Claim(ClaimTypes.Email, users.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(encodeKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}