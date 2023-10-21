using Microsoft.AspNetCore.Http;

namespace CRM.Core.Extensions;

public static class HttpContextAcessorExtensions
{
    public static int? ObterUsuarioId(this IHttpContextAccessor? contextAccessor)
    {
        var id = contextAccessor?.HttpContext?.User.ObterUsuarioId() ?? string.Empty;
        return string.IsNullOrWhiteSpace(id) ? null : int.Parse(id);
    }
    
    public static string ObterUsuarioNome(this IHttpContextAccessor? contextAccessor)
        => contextAccessor?.HttpContext?.User.ObterUsuarioNome() ?? string.Empty;

    public static string ObterUsuarioEmail(this IHttpContextAccessor? contextAccessor)
        => contextAccessor?.HttpContext?.User.ObterUsuarioEmail() ?? string.Empty;
    
    public static bool UsuarioAutenticado(this IHttpContextAccessor? contextAccessor)
    {
        return contextAccessor?.HttpContext?.User.UsuarioAutenticado() ?? false;
    }
}