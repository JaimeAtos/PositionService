using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;

namespace Application.Consumers.CatalogLevelConsumer;

public class CatalogLevelDeletedConsumer : IConsumer<CatalogLevelUpdated>
{
	private readonly IPositionRepository _repository;

	public CatalogLevelDeletedConsumer(IPositionRepository repository)
	{
		_repository = repository;
	}

	public async Task Consume(ConsumeContext<CatalogLevelUpdated> context)
	{
		var message = context.Message;
		var param = new Dictionary<string, object> { { "CatalogLevelId", message.LevelId } };
		var positions = await _repository.GetAllAsync(param);

		foreach (var position in positions)
		{
			await _repository.DeleteAsync(position.Id);
		}
	}
}
