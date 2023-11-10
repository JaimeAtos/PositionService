using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Consumers.CatalogLevelConsumers;
using Application.Consumers.ClientConsumers;
using Application.Consumers.ResourceConsumers;
using Application.Consumers.SkillConsumers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.Commons.Publishers;
using Atos.Core.EventsDTO;
using MassTransit;

namespace Application;

public static class DependencyContainer
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		services.AddScoped<IPublisherCommands<PositionCreated>, PublisherCommands<PositionCreated>>();
		services.AddScoped<IPublisherCommands<PositionUpdated>, PublisherCommands<PositionUpdated>>();
		services.AddScoped<IPublisherCommands<PositionDeleted>, PublisherCommands<PositionDeleted>>();

		services.AddScoped<IPublisherCommands<PositionSkillCreated>, PublisherCommands<PositionSkillCreated>>();
		services.AddScoped<IPublisherCommands<PositionSkillUpdated>, PublisherCommands<PositionSkillUpdated>>();
		services.AddScoped<IPublisherCommands<PositionSkillDeleted>, PublisherCommands<PositionSkillDeleted>>();

		services.AddScoped<IPublisherCommands<ResourcePositionCreated>, PublisherCommands<ResourcePositionCreated>>();
		services.AddScoped<IPublisherCommands<ResourcePositionUpdated>, PublisherCommands<ResourcePositionUpdated>>();
		services.AddScoped<IPublisherCommands<ResourcePositionDeleted>, PublisherCommands<ResourcePositionDeleted>>();

		services.AddMassTransitConfig();
		
		return services;
	}

	public static IServiceCollection AddMassTransitConfig(this IServiceCollection services)
	{
		
		services.AddMassTransit(cfg =>
		{
			cfg.AddConsumer<SkillUpdatedConsumer>();
			cfg.AddConsumer<SkillDeletedConsumer>();

			cfg.AddConsumer<ResourceUpdatedConsumer>();
			cfg.AddConsumer<ResourceDeletedConsumer>();

			cfg.AddConsumer<CatalogLevelUpdatedConsumer>();
			cfg.AddConsumer<CatalogLevelDeletedConsumer>();

			cfg.AddConsumer<ClientUpdatedConsumer>();
			cfg.AddConsumer<ClientDeletedConsumer>();

			cfg.UsingRabbitMq((ctx, cfgrmq) =>
			{
				cfgrmq.Host(GetMessageBrokerUrl());


				cfgrmq.ReceiveEndpoint("PositionServiceQueue", configureEndpoint =>
				{
					configureEndpoint.ConfigureConsumeTopology = false;
					configureEndpoint.Durable = true;

					configureEndpoint.ConfigureConsumer<SkillUpdatedConsumer>(ctx);
					configureEndpoint.ConfigureConsumer<SkillDeletedConsumer>(ctx);

					configureEndpoint.ConfigureConsumer<ResourceUpdatedConsumer>(ctx);
					configureEndpoint.ConfigureConsumer<ResourceDeletedConsumer>(ctx);

					configureEndpoint.ConfigureConsumer<CatalogLevelUpdatedConsumer>(ctx);
					configureEndpoint.ConfigureConsumer<CatalogLevelDeletedConsumer>(ctx);

					configureEndpoint.ConfigureConsumer<ClientUpdatedConsumer>(ctx);
					configureEndpoint.ConfigureConsumer<ClientDeletedConsumer>(ctx);

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

					configureEndpoint.Bind("Atos.Core.EventsDTO:CatalogLevelUpdated", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "catalog.level.updated";
					});

					configureEndpoint.Bind("Atos.Core.EventsDTO:CatalogLevelDeleted", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "catalog.level.deleted";
					});

					configureEndpoint.Bind("Atos.Core.EventsDTO:ClientUpdated", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "client.updated";
					});

					configureEndpoint.Bind("Atos.Core.EventsDTO:ClientDeleted", d =>
					{
						d.ExchangeType = "topic";
						d.RoutingKey = "client.deleted";
					});
				});

				cfgrmq.Publish<PositionCreated>(x => { x.ExchangeType = "topic"; });
				cfgrmq.Publish<PositionUpdated>(x => { x.ExchangeType = "topic"; });
				cfgrmq.Publish<PositionDeleted>(x => { x.ExchangeType = "topic"; });

				cfgrmq.Publish<PositionSkillCreated>(x => { x.ExchangeType = "topic"; });
				cfgrmq.Publish<PositionSkillUpdated>(x => { x.ExchangeType = "topic"; });
				cfgrmq.Publish<PositionSkillDeleted>(x => { x.ExchangeType = "topic"; });

				cfgrmq.Publish<ResourcePositionCreated>(x => { x.ExchangeType = "topic"; });
				cfgrmq.Publish<ResourcePositionUpdated>(x => { x.ExchangeType = "topic"; });
				cfgrmq.Publish<ResourcePositionDeleted>(x => { x.ExchangeType = "topic"; });
			});
		});
		return services;
	}

	private static string GetMessageBrokerUrl()
	{
		var messageBrokerHost = Environment.GetEnvironmentVariable("MQHOST");
		var messageBrokerPort = Environment.GetEnvironmentVariable("MQPORT");
		var user = Environment.GetEnvironmentVariable("MQUSER");
		var password = Environment.GetEnvironmentVariable("MQPASSWORD");
		var url = $"amqp://{user}:{password}@{messageBrokerHost}:{messageBrokerPort}";
		return url;
	}
}
