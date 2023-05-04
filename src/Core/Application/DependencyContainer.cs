using Application.Features.Positions.Commands.CreatePositionCommand;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Consumers.SkillConsumer;
using Atos.Core.EventsDTO;
using MassTransit;

namespace Application;

public static class DependencyContainer
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<CreatePositionCommand>());
		services.AddMassTransit(cfg =>
		{
			cfg.AddConsumer<SkillUpdatedConsumer>();
			
			cfg.UsingRabbitMq((ctx, cfgrmq) =>
			{
				cfgrmq.Host("amqp://guest:guest@localhost:5672");
				
				
				cfgrmq.ReceiveEndpoint("PositionServiceQueue", configureEndpoint =>
				{
					configureEndpoint.ConfigureConsumeTopology = false;
					configureEndpoint.Durable = true;
					
					configureEndpoint.ConfigureConsumer<SkillUpdatedConsumer>(ctx);
					
					configureEndpoint.UseMessageRetry(retryConfigure =>
					{
						retryConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
					});
					
					
					configureEndpoint.Bind("Atos.Core.EventsDTO:SkillUpdated", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "skill.updated";
					});
					
					configureEndpoint.Bind("Atos.Core.EventsDTO:SkillDeleted", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "skill.deleted";
					});
					
					configureEndpoint.Bind("Atos.Core.EventsDTO:ResourceUpdated", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "resource.updated";
					});
					
					configureEndpoint.Bind("Atos.Core.EventsDTO:ResourceDeleted", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "resource.deleted";
					});
				});
				
				cfgrmq.Publish<PositionCreated>(x =>
				{
					x.ExchangeType = "topic";
				});
				cfgrmq.Publish<PositionUpdated>(x =>
				{
					x.ExchangeType = "topic";
				});
				cfgrmq.Publish<PositionDeleted>(x =>
				{
					x.ExchangeType = "topic";
				});
				
				cfgrmq.Publish<PositionSkillCreated>(x =>
				{
					x.ExchangeType = "topic";
				});
				cfgrmq.Publish<PositionSkillUpdated>(x =>
				{
					x.ExchangeType = "topic";
				});
				cfgrmq.Publish<PositionSkillDeleted>(x =>
				{
					x.ExchangeType = "topic";
				});
				
				cfgrmq.Publish<ResourcePositionCreated>(x =>
				{
					x.ExchangeType = "topic";
				});
				cfgrmq.Publish<ResourcePositionUpdated>(x =>
				{
					x.ExchangeType = "topic";
				});
				cfgrmq.Publish<ResourcePositionDeleted>(x =>
				{
					x.ExchangeType = "topic";
				});
			});
			
		});
        return services;
    }
}
