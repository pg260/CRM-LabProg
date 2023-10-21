using System.Security.Claims;

namespace CRM.Core.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string? ObterUsuarioId(this ClaimsPrincipal? principal) 
        => GetClaim(principal, ClaimTypes.NameIdentifier);

    public static string? ObterUsuarioNome(this ClaimsPrincipal? principal)
        => GetClaim(principal, ClaimTypes.Name);
    
    public static string? ObterUsuarioEmail(this ClaimsPrincipal? principal)
        => GetClaim(principal, ClaimTypes.Email);
    
    public static bool UsuarioAutenticado(this ClaimsPrincipal? principal)
        => principal?.Identity?.IsAuthenticated ?? false;

    private static string? GetClaim(ClaimsPrincipal? principal, string claimName)
    {
        if (principal == null) throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(claimName);
        return claim?.Value;
    }
}