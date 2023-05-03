using Atos.Core.EventsDTO;
using MassTransit;

namespace SkillConsumer.SkillChanged;

public class SkillDeletedConsumer : IConsumer<SkillDeleted>
{
	public Task Consume(ConsumeContext<SkillDeleted> context)
	{
		throw new NotImplementedException();
	}
}