using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.ResourcePosition;

[ApiVersion("1.0")]
public class ReadResourcePositionController : BaseApiController
{
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetResourcePositionById(Guid id)
	{
		return Ok(await Mediator.Send(new {Id = id}));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllResourcePositions()
	{
		return Ok(await Mediator.Send(new { }));
	}
}