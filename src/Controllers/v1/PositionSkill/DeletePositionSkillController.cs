using Application.Features.PositionSkills.Commands.DeletePositionSkillCommand;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class DeletePositionSkillController : BaseApiController
{
    [HttpDelete]
    public Task<IActionResult> DeletePositionSkill(DeletePositionSkillCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
            throw new ArgumentNullException();

        return ProcessDeletePositionSkill(command, cancellationToken);
    }
    private async Task<IActionResult> ProcessDeletePositionSkill(DeletePositionSkillCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
}