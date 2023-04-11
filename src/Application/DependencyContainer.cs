using Application.Features.Positions.Commands.CreatePositionCommand;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyContainer
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<CreatePositionCommand>());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
