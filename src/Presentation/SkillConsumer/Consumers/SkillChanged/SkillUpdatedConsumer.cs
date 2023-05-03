using Atos.Core.EventsDTO;
using MassTransit;

namespace SkillConsumer.Consumers.SkillChanged;

public class SkillUpdatedConsumer : IConsumer<SkillUpdated>
{
	public Task Consume(ConsumeContext<SkillUpdated> context)
	{
		throw new NotImplementedException();
	}
}