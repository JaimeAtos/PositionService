using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace PositionConsumers;

public static class DependencyContainer
{
	public static IServiceCollection AddConsumers(this IServiceCollection services)
	{
		services.AddMassTransit(config =>
		{
			config.AddConsumer<PositionCreatedConsumer>();
			
			config.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host("amqp://guest:guest@localhost:5672");
				cfg.ReceiveEndpoint("Atos.Core.EventsDTO:PositionCreated", econfigureEndpoint =>
				{
					econfigureEndpoint.Durable = true;
					econfigureEndpoint.UseMessageRetry(retryConfigure =>
					{
						retryConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					econfigureEndpoint.ConfigureConsumer<PositionCreatedConsumer>(ctx);
				});
			});
		});
		return services;
	}
}