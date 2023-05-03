using Atos.Core.EventsDTO;
using MassTransit;

namespace SkillConsumer.SkillChanged;

public class SkillUpdatedConsumer : IConsumer<SkillUpdated>
{
	public Task Consume(ConsumeContext<SkillUpdated> context)
	{
		throw new NotImplementedException();
	}
}