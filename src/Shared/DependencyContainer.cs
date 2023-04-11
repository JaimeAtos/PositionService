using Domain.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared;

public static class DependencyContainer 
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IDateTimeService, DateTimeService>();

        return services;
    }
}
