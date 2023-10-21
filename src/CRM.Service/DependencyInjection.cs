using System.Reflection;
using AutoMapper;
using CRM.Infra;
using CRM.Service.Contracts;
using CRM.Service.MapperConfig;
using CRM.Service.NotificatorConfig;
using CRM.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Service;

public static class DependencyInjection
{
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        services.AddRepositories();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<INotificator, Notificator>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void CreateAutomapper(this IServiceCollection services)
    {
        var provider = CreateServiceProvider();
        
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.ConstructServicesUsing(provider.GetService);
            mc.AddProfile(new AutoMapperProfile());
        });
        
        mappingConfig.CreateMapper();
    }
    
    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        return services.BuildServiceProvider();
    }
}