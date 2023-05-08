using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;

namespace Application.Consumers.ClientConsumers;

public class ClientUpdatedConsumer : IConsumer<ClientUpdated>
{
	private readonly IPositionRepository _repository;

	public ClientUpdatedConsumer(IPositionRepository repository)
	{
		_repository = repository;
	}

	public async Task Consume(ConsumeContext<ClientUpdated> context)
	{
		var message = context.Message;
		var param = new Dictionary<string, object>{{"ClientId", message.Id}};
		var positions = await _repository.GetAllAsync(param);

		foreach (var position in positions)
		{
			position.ClientDescription = message.Name;
			await _repository.UpdateAsync(position, position.Id);
		}
	}
}
