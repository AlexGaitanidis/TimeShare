using ErrorOr;
using Mapster;
using MapsterMapper;
using System.Reflection;
using TimeShare.App.Common;

namespace TimeShare.App;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<ProblemDetailsOptions>(options =>
            options.CustomizeProblemDetails = context =>
            {
                if (context.HttpContext.Items[HttpContextItemKeys.Errors] is List<Error> errors)
                {
                    context.ProblemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
                }
            });

        services.AddMappings();

        return services;
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}