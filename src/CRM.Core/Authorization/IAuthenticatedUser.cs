namespace CRM.Core.Authorization;

public interface IAuthenticatedUser
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}