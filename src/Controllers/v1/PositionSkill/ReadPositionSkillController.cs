using Application.Features.PositionSkills.Queries.GetAllPositionSkill;
using Application.Features.PositionSkills.Queries.GetPositionSkillById;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class ReadPositionSkillController : BaseApiController
{
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetPositionSkillById(Guid id)
	{
		return Ok(await Mediator.Send(new GetPositionSkillByIdQuery{ Id = id }));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllPositionSkills([FromQuery]GetAllPositionSkillParameters filters)
	{
		return Ok(await Mediator.Send(new GetAllPositionSkillQuery()
		{
			// Id = filters.Id,
			// State = filters.State,
			// MinToAccept = filters.MinToAccept,
			PageNumber = filters.PageNumber,
			PageSize = filters.PageSize,
			// PositionId = filters.PositionId,
			// PositionSkillType = filters.PositionSkillType,
			// SkillId = filters.SkillId,
			// SkillName = filters.SkillName
		}));
	}
}
