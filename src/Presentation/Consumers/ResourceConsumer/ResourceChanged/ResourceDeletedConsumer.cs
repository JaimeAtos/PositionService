using Atos.Core.EventsDTO;
using MassTransit;

namespace ResourceConsumer.ResourceChanged;

public class ResourceDeletedConsumer : IConsumer<ResourceDeleted>
{
	public Task Consume(ConsumeContext<ResourceDeleted> context)
	{
		throw new NotImplementedException();
	}
}