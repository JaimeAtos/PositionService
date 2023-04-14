using Application.Exceptions;
using Application.Features.PositionSkills.Commands.DeletePositionSkillCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class DeletePositionSkillController : BaseApiController
{
	public DeletePositionSkillController(IMediator mediator) : base(mediator)
	{
	}
	[HttpDelete]
	public Task<IActionResult> DeletePositionSkill(DeletePositionSkillCommand command,
		CancellationToken cancellationToken = default)
	{
		if (command is null)
		{
			throw new ApiException("Body request is empty");
		}

		return ProcessDeletePositionSkill(command, cancellationToken);
	}

	private async Task<IActionResult> ProcessDeletePositionSkill(DeletePositionSkillCommand command,
		CancellationToken cancellationToken = default)
	{
		await Mediator.Send(command, cancellationToken);
		return NoContent();
	}

}