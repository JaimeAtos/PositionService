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
		

		return services;
	}
}