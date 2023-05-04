using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;

namespace Application.Consumers.SkillConsumer;

public class SkillDeletedConsumer : IConsumer<SkillDeleted>
{
	private readonly IPositionSkillRepository _repository;

	public SkillDeletedConsumer(IPositionSkillRepository repository)
	{
		_repository = repository;
	}

	public async Task Consume(ConsumeContext<SkillDeleted> context)
	{
		var message = context.Message;
		var param = new Dictionary<string, object> { { "SkillId", message.Id } };

		var positionSkills = await _repository.GetAllAsync(param);

		foreach (var positionSkill in positionSkills)
		{
			await _repository.DeleteAsync(positionSkill.Id);
		}
	}
}
