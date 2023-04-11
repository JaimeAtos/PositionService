using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class ReadPositionSkillController : BaseApiController
{
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetPositionSkillById(Guid id)
	{
		return Ok(await Mediator.Send(new { Id = id }));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllPositionSkills()
	{
		return Ok(await Mediator.Send(new { }));
	}
}
