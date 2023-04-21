using Application.Exceptions;
using Application.Features.Positions.Commands.CreatePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class CreatePositionController : BaseApiController
{
	public CreatePositionController(IMediator mediator) : base(mediator)
	{
	}
	
	[HttpPost]
	public Task<IActionResult> CreatePosition(CreatePositionCommand command, CancellationToken cancellation = default)
	{
		if (command is null)
			throw new ApiException("Body request is empty");

		return ProcessCreatePosition(command, cancellation);
	}

	private async Task<IActionResult> ProcessCreatePosition(CreatePositionCommand command,
		CancellationToken cancellationToken = default)
	{
		var result = await Mediator.Send(command, cancellationToken);
		return CreatedAtRoute("GetPositionById", routeValues: new {id = result}, command);

	}
}
