using Atos.Core.EventsDTO;
using MassTransit;

namespace ResourceConsumer.Consumers.ResourceChanged;

public class ResourceUpdatedConsumer : IConsumer<ResourceUpdated>
{
	public Task Consume(ConsumeContext<ResourceUpdated> context)
	{
		throw new NotImplementedException();
	}
}