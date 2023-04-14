using Application.Features.PositionSkills.Queries.GetAllPositionSkill;
using Application.Features.PositionSkills.Queries.GetPositionSkillById;
using Application.Parameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class ReadPositionSkillController : BaseApiController
{
	public ReadPositionSkillController(IMediator mediator) : base(mediator)
	{
	}
	
	[HttpGet("{id}", Name = "GetPositionSkillById")]
	public async Task<IActionResult> GetPositionSkillById(Guid id)
	{
		return Ok(await Mediator.Send(new GetPositionSkillByIdQuery{ Id = id }));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllPositionSkills([FromQuery]RequestParameter filters)
	{
		return Ok(await Mediator.Send(new GetAllPositionSkillQuery()
		{
			PageNumber = filters.PageNumber,
			PageSize = filters.PageSize,
		}));
	}

}
