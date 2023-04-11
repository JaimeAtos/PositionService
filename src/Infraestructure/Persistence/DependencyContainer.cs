using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyContainer
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSingleton(new PositionDbContext(connectionString));
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IPositionSkillRepository, PositionSkillRepository>();
        services.AddScoped<IResourcePositionRepository, ResourcePositionRepository>();
        return services;
    }
}
