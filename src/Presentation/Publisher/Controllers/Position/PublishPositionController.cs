using Atos.Core.EventsDTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Publisher.Controllers.Position;

[Route("api/[controller]")]
[ApiController]
public class PublishPositionController : ControllerBase
{
	private readonly IPublishEndpoint _publishEndpoint;

	public PublishPositionController(IPublishEndpoint publishEndpoint)
	{
		_publishEndpoint = publishEndpoint;
	}

	[HttpPost]
	public async Task<IActionResult> PositionCreated(PositionCreated position, CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(position, ctx =>
		{
			ctx.MessageId = Guid.NewGuid();
		}, cancellationToken);
		return Ok();
	}
	
	[HttpPost]
	public async Task<IActionResult> PositionUpdated(PositionUpdated position, CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(position, ctx =>
		{
			ctx.MessageId = Guid.NewGuid();
		}, cancellationToken);
		return Ok();
	}
	
	[HttpPost]
	public async Task<IActionResult> PositionDeleted(PositionDeleted position, CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(position, ctx =>
		{
			ctx.MessageId = Guid.NewGuid();
		}, cancellationToken);
		return Ok();
	}
}
