using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SkillConsumer.SkillChanged;

namespace SkillConsumer;

public static class DependencyContainer
{
	public static IServiceCollection AddSkillConsumer(this IServiceCollection services)
	{
		services.AddMassTransit(config =>
		{
			config.AddConsumer<SkillCreatedConsumer>();
			config.AddConsumer<SkillUpdatedConsumer>();
			config.AddConsumer<SkillDeletedConsumer>();
			
			config.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host("amqp://guest:guest@localhost:5672");
				cfg.ReceiveEndpoint("skill.created", econfigureEndpoint =>
				{
					econfigureEndpoint.Durable = true;
					econfigureEndpoint.UseMessageRetry(retryConfigure =>
					{
						retryConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					econfigureEndpoint.ConfigureConsumer<SkillCreatedConsumer>(ctx);
				});
				
				cfg.ReceiveEndpoint("skill.updated", econfigureEndpoint =>
				{
					econfigureEndpoint.Durable = true;
					econfigureEndpoint.UseMessageRetry(retrayConfigure =>
					{
						retrayConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					econfigureEndpoint.ConfigureConsumer<SkillUpdatedConsumer>(ctx);
				});
				
				cfg.ReceiveEndpoint("skill.deleted", econfigureEndpoint =>
				{
					econfigureEndpoint.Durable = true;
					econfigureEndpoint.UseMessageRetry(retrayConfigure =>
					{
						retrayConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					econfigureEndpoint.ConfigureConsumer<SkillDeletedConsumer>(ctx);
				});
			});
		});
		return services;
	}
}