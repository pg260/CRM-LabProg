namespace CRM.Service.Dtos.UserDtos;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string? Nome { get; set; } 
    public string? Email { get; set; }
    public string? Foto { get; set; }
}