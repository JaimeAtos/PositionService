using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;

namespace Application.Consumers.SkillConsumers;

public class SkillUpdatedConsumer : IConsumer<SkillUpdated>
{
	private readonly IPositionSkillRepository _repository;

	public SkillUpdatedConsumer(IPositionSkillRepository repository)
	{
		_repository = repository;
	}


	public async Task Consume(ConsumeContext<SkillUpdated> context)
	{
		var message = context.Message;
		var param = new Dictionary<string, object> { { "SkillId", message.Id } };

		var positionSkills = await _repository.GetAllAsync(param);

		foreach (var positionSkill in positionSkills)
		{
			positionSkill.SkillName = message.Description;
			await _repository.UpdateAsync(positionSkill, positionSkill.Id);
		}
	}
}
