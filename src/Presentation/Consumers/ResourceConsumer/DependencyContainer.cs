using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ResourceConsumer.ResourceChanged;

namespace ResourceConsumer;

public static class DependencyContainer
{
	public static IServiceCollection AddResourceConsumer(this IServiceCollection services)
	{
		
		services.AddMassTransit(config =>
		{
			config.AddConsumer<ResourceCreatedConsumer>();
			config.AddConsumer<ResourceUpdatedConsumer>();
			config.AddConsumer<ResourceDeletedConsumer>();
			
			config.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host("amqp://guest:guest@localhost:5672");
				cfg.ReceiveEndpoint("resource.created", econfigureEndpoint =>
				{
					econfigureEndpoint.Durable = true;
					econfigureEndpoint.UseMessageRetry(retrayConfigure =>
					{
						retrayConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					econfigureEndpoint.ConfigureConsumer<ResourceCreatedConsumer>(ctx);
				});
				
				cfg.ReceiveEndpoint("resource.updated", econfigureEndpoint =>
				{
					econfigureEndpoint.Durable = true;
					econfigureEndpoint.UseMessageRetry(retrayConfigure =>
					{
						retrayConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					econfigureEndpoint.ConfigureConsumer<ResourceUpdatedConsumer>(ctx);
				});
				
				cfg.ReceiveEndpoint("Resource.deleted", econfigureEndpoint =>
				{
					econfigureEndpoint.Durable = true;
					econfigureEndpoint.UseMessageRetry(retrayConfigure =>
					{
						retrayConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					econfigureEndpoint.ConfigureConsumer<ResourceDeletedConsumer>(ctx);
				});
			});
		});
		return services;
	}
}
