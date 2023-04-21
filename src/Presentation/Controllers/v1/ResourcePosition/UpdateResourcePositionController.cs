using Application.Exceptions;
using Application.Features.ResourcePositions.Commands.UpdateResourcePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.ResourcePosition;

[ApiVersion("1.0")]
public class UpdateResourcePositionController : BaseApiController
{
	public UpdateResourcePositionController(IMediator mediator) : base(mediator)
	{
	}
	
	[HttpPut]
	public Task<IActionResult> UpdateResourcePosition(UpdateResourcePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		if (command is null)
		{
			throw new ApiException("Body request is empty");
		}

		return ProcessUpdateResourcePosition(command, cancellationToken);
	}

	private async Task<IActionResult> ProcessUpdateResourcePosition(UpdateResourcePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		await Mediator.Send(command, cancellationToken);
		return NoContent();
	}

}
