using Atos.Core.EventsDTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Publisher.Controllers.ResourcePosition;

[Route("api/[controller]")]
[ApiController]
public class PublishResourcePositionController : ControllerBase
{
	private readonly IPublishEndpoint _publishEndpoint;

	public PublishResourcePositionController(IPublishEndpoint publishEndpoint)
	{
		_publishEndpoint = publishEndpoint;
	}

	[HttpPost]
	public async Task<IActionResult> ResourcePositionCreated(ResourcePositionCreated resourcePosition,
		CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(resourcePosition, ctx =>
		{
			ctx.MessageId = Guid.NewGuid();
		}, cancellationToken);
		return Ok();
	}
	
	[HttpPost]
	public async Task<IActionResult> ResourcePositionUpdated(ResourcePositionUpdated resourcePosition,
		CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(resourcePosition, ctx =>
		{
			ctx.MessageId = Guid.NewGuid();
		}, cancellationToken);
		return Ok();
	}
	
	[HttpPost]
	public async Task<IActionResult> ResourcePositionDeleted(ResourcePositionDeleted resourcePosition,
		CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(resourcePosition, ctx =>
		{
			ctx.MessageId = Guid.NewGuid();
		}, cancellationToken);
		return Ok();
	}
}
