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
        var host = Environment.GetEnvironmentVariable("DBHOST");
        var port = Environment.GetEnvironmentVariable("DBPORT");
        var user = Environment.GetEnvironmentVariable("DBUSER");
        var password = Environment.GetEnvironmentVariable("DBPASSWORD");
        var dbname = Environment.GetEnvironmentVariable("DBNAME");
        var connectionString = $"Username={user};Password={password};Host={host};Port={port};Database={dbname};";
        services.AddSingleton(new PositionDbContext(connectionString));
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IPositionSkillRepository, PositionSkillRepository>();
        services.AddScoped<IResourcePositionRepository, ResourcePositionRepository>();
        return services;
    }
}
