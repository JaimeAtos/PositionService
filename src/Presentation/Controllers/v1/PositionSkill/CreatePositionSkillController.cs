using Application.Exceptions;
using Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class CreatePositionSkillController : BaseApiController
{
	public CreatePositionSkillController(IMediator mediator) : base(mediator)
	{
	}

	[HttpPost]
	public Task<IActionResult> CreatePositionSkill(CreatePositionSkillCommand command,
		CancellationToken cancellation = default)
	{
		if (command is null)
		{
			throw new ApiException("Body request is empty");
		}

		return ProcessCreatePositionSkill(command, cancellation);
	}

	private async Task<IActionResult> ProcessCreatePositionSkill(CreatePositionSkillCommand command,
		CancellationToken cancellation = default)
	{
		var result = await Mediator.Send(command, cancellation);
		return CreatedAtRoute("GetPositionSkillById", new { id = result.Data }, command);
	}
}
