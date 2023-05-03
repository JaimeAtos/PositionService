using Atos.Core.EventsDTO;
using MassTransit;

namespace ResourceConsumer.ResourceChanged;

public class ResourceUpdatedConsumer : IConsumer<ResourceUpdated>
{
	public Task Consume(ConsumeContext<ResourceUpdated> context)
	{
		throw new NotImplementedException();
	}
}