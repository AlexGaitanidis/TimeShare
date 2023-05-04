using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Persistence.Interceptors;
using TimeShare.Persistence.Repositories;

namespace TimeShare.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO maybe move interceptors to UnitOfWork
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<TimeShareDbContext>((sp, options) =>
        {
            var outboxInterceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!;
            var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;

            options.UseSqlServer(configuration.GetConnectionString("Default"))
                .AddInterceptors(outboxInterceptor, auditableInterceptor);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHostRepository, HostRepository>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IMeetingReviewRepository, MeetingReviewRepository>();

        return services;
    }
}