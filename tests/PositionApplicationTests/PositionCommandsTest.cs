using Application.Features.Positions.Commands.CreatePositionCommand;
using Application.Wrappers;
using Controllers.v1.Position;
using MediatR;
using Moq;

namespace PositionApplication;

public class PositionCommandsTest
{
	[Fact]
	public void PositionCreateCommandTest()
	{
		var mediator = new Mock<IMediator>();
		
		
		mediator.Setup(m =>
				m.Send(It.IsAny<CreatePositionCommand>(),
					It.IsAny<CancellationToken>())
			)
			.ReturnsAsync(() => new Response<Guid>(true, Guid.Empty))
			.Verifiable("Notification was not sent.");

		var controller = new CreatePositionController(mediator.Object);
		controller.CreatePosition(new CreatePositionCommand());

		mediator.Verify(m =>
			m.Send(
				It.IsAny<CreatePositionCommand>(),
				It.IsAny<CancellationToken>()
			)
		);
	}
}