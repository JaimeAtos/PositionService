using Atos.Core.EventsDTO;
using MassTransit;

namespace PositionConsumers;

public class PositionCreatedConsumer : IConsumer<PositionCreated>
{
	public Task Consume(ConsumeContext<PositionCreated> context)
	{
		var messageId = context.MessageId.ToString();
		var message = context.Message;
		Console.WriteLine(messageId);
		Console.WriteLine(message.Description);
		return Task.CompletedTask;
	}
}