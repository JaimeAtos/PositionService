using Atos.Core.EventsDTO;
using MassTransit;

namespace SkillConsumer.SkillChanged;

public class SkillCreatedConsumer : IConsumer<SkillCreated>
{
	public Task Consume(ConsumeContext<SkillCreated> context)
	{
		throw new NotImplementedException();
	}
}