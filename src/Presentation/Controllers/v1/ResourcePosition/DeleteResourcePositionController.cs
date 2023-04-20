using Application.Exceptions;
using Application.Features.ResourcePositions.Commands.DeleteResourcePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.ResourcePosition;

[ApiVersion("1.0")]
public class DeleteResourcePositionController : BaseApiController
{
	public DeleteResourcePositionController(IMediator mediator) : base(mediator)
	{
	}
	
	[HttpDelete]
	public Task<IActionResult> DeleteResourcePosition(DeleteResourcePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		if (command is null)
		{
			throw new ApiException("Body request is empty");
		}

		return ProcessDeleteResourcePosition(command, cancellationToken);
	}

	private async Task<IActionResult> ProcessDeleteResourcePosition(DeleteResourcePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		await Mediator.Send(command, cancellationToken);
		return NoContent();
	}

}