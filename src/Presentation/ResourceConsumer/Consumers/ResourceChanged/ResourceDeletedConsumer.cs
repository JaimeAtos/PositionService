using Atos.Core.EventsDTO;
using MassTransit;

namespace ResourceConsumer.Consumers.ResourceChanged;

public class ResourceDeletedConsumer : IConsumer<ResourceDeleted>
{
	public Task Consume(ConsumeContext<ResourceDeleted> context)
	{
		throw new NotImplementedException();
	}
}