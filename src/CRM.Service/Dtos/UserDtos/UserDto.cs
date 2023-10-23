namespace CRM.Service.Dtos.UserDtos;

public class UserDto
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Foto { get; set; }
}