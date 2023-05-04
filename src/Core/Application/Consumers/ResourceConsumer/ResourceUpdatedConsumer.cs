using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;

namespace Application.Consumers.ResourceConsumer;

public class ResourceUpdatedConsumer : IConsumer<ResourceUpdated>
{
	private readonly IResourcePositionRepository _repository;

	public ResourceUpdatedConsumer(IResourcePositionRepository repository)
	{
		_repository = repository;
	}

	public async Task Consume(ConsumeContext<ResourceUpdated> context)
	{
		var message = context.Message;
		var param = new Dictionary<string, object> { { "ResourceId", message.Id } };

		var resourcePositions = await _repository.GetAllAsync(param);

		foreach (var resourcePosition in resourcePositions)
		{
			resourcePosition.ResourceName = message.ResourceName;
			await _repository.UpdateAsync(resourcePosition, resourcePosition.Id);
		}
	}
}
