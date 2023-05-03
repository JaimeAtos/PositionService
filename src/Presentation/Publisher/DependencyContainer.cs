using MassTransit;

namespace Publisher;
	
public static class DependencyContainer
{
	public static IServiceCollection AddPublisher(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddMassTransit(cfg =>
		{
			cfg.UsingRabbitMq((ctx, cfg1) => { cfg1.Host("amqp://guest@localhost:5672"); });
		});
		return services;
	}
}