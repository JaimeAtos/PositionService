using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;

namespace Application.Consumers.ClientConsumer;

public class ClientDeletedConsumer : IConsumer<ClientDeleted>
{
	private readonly IPositionRepository _repository;

	public ClientDeletedConsumer(IPositionRepository repository)
	{
		_repository = repository;
	}

	public async Task Consume(ConsumeContext<ClientDeleted> context)
	{
		var message = context.Message;
		var param = new Dictionary<string, object>{{"ClientId", message.Id}};
		var positions = await _repository.GetAllAsync(param);

		foreach (var position in positions)
		{
			await _repository.DeleteAsync(position.Id);
		}
	}
}
