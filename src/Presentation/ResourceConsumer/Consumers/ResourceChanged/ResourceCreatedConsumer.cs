using Atos.Core.EventsDTO;
using MassTransit;

namespace ResourceConsumer.Consumers.ResourceChanged;

public class ResourceCreatedConsumer : IConsumer<ResourceCreated>
{
	public Task Consume(ConsumeContext<ResourceCreated> context)
	{
		throw new NotImplementedException();
	}
}