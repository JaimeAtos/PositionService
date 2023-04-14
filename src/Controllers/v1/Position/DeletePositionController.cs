using Application.Exceptions;
using Application.Features.Positions.Commands.DeletePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class DeletePositionController : BaseApiController
{
	[HttpDelete]
	public Task<IActionResult> DeletePosition(DeletePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		if (command is null)
			throw new ApiException("Body request is empty");

		return ProcessDeletePosition(command, cancellationToken);
	}

	private async Task<IActionResult> ProcessDeletePosition(DeletePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		await Mediator.Send(command, cancellationToken);
		return NoContent();
	}

	public DeletePositionController(IMediator mediator) : base(mediator)
	{
	}
}