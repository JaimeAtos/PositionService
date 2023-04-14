using System.Reflection;
using Application.Features.Positions.Commands.CreatePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Controllers;

public static class DependencyContainer
{
	public static IServiceCollection AddApiVersioning(this IServiceCollection services)
	{
		services.AddApiVersioning(cfg =>
		{
			cfg.DefaultApiVersion = new ApiVersion(1, 0);
			cfg.AssumeDefaultVersionWhenUnspecified = true;
			cfg.ReportApiVersions = true;
		});
		
		services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<CreatePositionCommand>());

		return services;
	}
}