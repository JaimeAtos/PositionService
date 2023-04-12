using Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class CreatePositionSkillController : BaseApiController
{
    [HttpPost]
    public Task<IActionResult> CreatePositionSkill(CreatePositionSkillCommand command, CancellationToken cancellation = default)
    {
        if (command is null)
            throw new ArgumentNullException();

        return ProcessCreatePositionSkill(command, cancellation);
    }

    private async Task<IActionResult> ProcessCreatePositionSkill(CreatePositionSkillCommand command, CancellationToken cancellation = default)
    {
        var result = await Mediator.Send(command, cancellation);
        return CreatedAtRoute("GetPositionSkillById", new {id = result}, command);
    }

}
