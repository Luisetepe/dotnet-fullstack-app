using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Template.Application.Data.DbContexts;
using WebApp.Template.Application.Services.Crypto;
using WebApp.Template.Application.Services.Identity;

namespace WebApp.Template.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<WebAppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("WebAppDb"));
            options.UseSnakeCaseNamingConvention();
        });

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services.AddSingleton<IUniqueIdentifierService, UniqueIdentifierService>();
        services.AddScoped<ICryptoService, CryptoService>();

        return services;
    }
}
