using Atos.Core.EventsDTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Publisher.Controllers.PositionSkill;

[Route("api/v{}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PublishPositionSkillController : ControllerBase
{
	private readonly IPublishEndpoint _publishEndpoint;

	public PublishPositionSkillController(IPublishEndpoint publishEndpoint)
	{
		_publishEndpoint = publishEndpoint;
	}

	[HttpPost]
	public async Task<IActionResult> PositionSkillCreated(PositionSkillCreated positionSkill,
		CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(positionSkill, ctx => { ctx.MessageId = Guid.NewGuid(); }, cancellationToken);
		return Ok();
	}
	
	[HttpPost]
	public async Task<IActionResult> PositionSkillUpdated(PositionSkillUpdated positionSkill,
		CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(positionSkill, ctx => { ctx.MessageId = Guid.NewGuid(); }, cancellationToken);
		return Ok();
	}
	
	[HttpPost]
	public async Task<IActionResult> PositionSkillDeleted(PositionSkillDeleted positionSkill,
		CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(positionSkill, ctx => { ctx.MessageId = Guid.NewGuid(); }, cancellationToken);
		return Ok();
	}
}