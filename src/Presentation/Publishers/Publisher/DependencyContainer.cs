using Atos.Core.EventsDTO;
using MassTransit;
using RabbitMQ.Client;

namespace Publisher;

public static class DependencyContainer
{
	public static IServiceCollection AddPublisher(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddMassTransit(cfg =>
		{
			cfg.UsingRabbitMq((ctx, cfgrmq) =>
			{
				cfgrmq.Host("amqp://guest:guest@localhost:5672");
			});
		});
		return services;
	}
}