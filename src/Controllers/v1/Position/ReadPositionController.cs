using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class ReadPositionController : BaseApiController
{
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetPositionById(Guid id)
	{
		return Ok(await Mediator.Send(new {Id = id}));
	}
}
