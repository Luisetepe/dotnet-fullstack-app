using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Data.Services;

namespace WebApp.Template.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WebAppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("WebAppDb"));
            options.UseSnakeCaseNamingConvention();
        });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddSingleton<IUniqueIdentifierService, UniqueIdentifierService>();

        return services;
    }
}