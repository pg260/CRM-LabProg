﻿namespace CRM.Service.Dtos.UserDtos;

public class CreateUserDto
{
    public string Nome  { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string? Foto { get; set; }
}