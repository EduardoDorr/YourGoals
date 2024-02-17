using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using FluentValidation.AspNetCore;

namespace YourGoals.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator()
                .AddValidator();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

    private static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
        services.AddFluentValidationAutoValidation();

        return services;
    }
}