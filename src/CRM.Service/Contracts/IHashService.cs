namespace CRM.Service.Contracts;

public interface IHashService
{
    string GerarHash(string password);
    bool VerificarHash(string password, string hash);
}