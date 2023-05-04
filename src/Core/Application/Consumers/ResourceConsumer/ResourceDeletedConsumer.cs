using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;

namespace Application.Consumers.ResourceConsumer;

public class ResourceDeletedConsumer : IConsumer<ResourceDeleted>
{
	private readonly IResourcePositionRepository _repository;

	public ResourceDeletedConsumer(IResourcePositionRepository repository)
	{
		_repository = repository;
	}

	public async Task Consume(ConsumeContext<ResourceDeleted> context)
	{
		var message = context.Message;
		var param = new Dictionary<string, object> { { "ResourceId", message.Id } };

		var resourcePositions = await _repository.GetAllAsync(param);

		foreach (var resourcePosition in resourcePositions)
		{
			await _repository.DeleteAsync(resourcePosition.Id);
		}
	}
}
