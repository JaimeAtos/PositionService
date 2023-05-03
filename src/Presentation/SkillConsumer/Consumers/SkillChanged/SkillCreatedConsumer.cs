
using Atos.Core.EventsDTO;
using MassTransit;

namespace SkillConsumer.Consumers.SkillChanged;

public class SkillCreatedConsumer : IConsumer<SkillCreated>
{
	public Task Consume(ConsumeContext<SkillCreated> context)
	{
		throw new NotImplementedException();
	}
}