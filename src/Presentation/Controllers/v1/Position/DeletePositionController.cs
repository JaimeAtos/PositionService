using Application.Exceptions;
using Application.Features.Positions.Commands.DeletePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class DeletePositionController : BaseApiController
{
	public DeletePositionController(IMediator mediator) : base(mediator)
	{
	}
	
	[HttpDelete]
	public Task<IActionResult> DeletePosition(DeletePositionCommandById commandById,
		CancellationToken cancellationToken = default)
	{
		if (commandById is null)
			throw new ApiException("Body request is empty");

		return ProcessDeletePosition(commandById, cancellationToken);
	}
	

	private async Task<IActionResult> ProcessDeletePosition(DeletePositionCommandById commandById,
		CancellationToken cancellationToken = default)
	{
		await Mediator.Send(commandById, cancellationToken);
		return NoContent();
	}
}
