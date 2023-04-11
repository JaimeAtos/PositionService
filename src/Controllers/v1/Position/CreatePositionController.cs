using Application.Features.Positions.Commands.CreatePositionCommand;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class CreatePositionController : BaseApiController
{
	[HttpPost]
	public Task<IActionResult> CreatePosition(CreatePositionCommand command, CancellationToken cancellation = default)
	{
		if (command is null)
			throw new ArgumentNullException();

		return ProcessCreatePosition(command, cancellation);
	}

	private async Task<IActionResult> ProcessCreatePosition(CreatePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		var result = await Mediator.Send(command, cancellationToken);
		return CreatedAtRoute("GetPositionById", new { id = result}, command);
	}
}