namespace CRM.Service.Dtos.AuthDtos;

public class LoginDto
{
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
}