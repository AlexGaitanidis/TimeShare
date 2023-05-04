using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TimeShare.Application.Behaviors;

namespace TimeShare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
                .AddOpenBehavior(typeof(ValidationBehavior<,>))
                .AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}