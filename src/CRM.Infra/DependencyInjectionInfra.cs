using CRM.Core.Authorization;
using CRM.Core.Extensions;
using CRM.Domain.Contracts.Repositories;
using CRM.Infra.Context;
using CRM.Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Infra;

public static class DependencyInjectionInfra
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticatedUser>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            return httpContextAccessor.UsuarioAutenticado() ? new AuthenticatedUser(httpContextAccessor) : new AuthenticatedUser();
        });
        
        services.AddDbContext<BaseDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
        services.AddScoped<IProdutoCarrinhoRepository, ProdutoCarrinhoRepository>();
        services.AddScoped<IHistoricoComprasRepository, HistoricoComprasRepository>();
    }

    public static void UseMigrations(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
        db.Database.Migrate();
    }
}