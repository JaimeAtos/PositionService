using Application.Features.PositionSkills.Commands.UpdatePositionSkillCommand;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class UpdatePositionSkillController : BaseApiController
{
  
    [HttpPut]
    public Task<IActionResult> UpdatePositionSkill(UpdatePositionSkillCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
            throw new ArgumentNullException();

        return ProcessUpdatePositionSkill(command, cancellationToken);
    }
    private async Task<IActionResult> ProcessUpdatePositionSkill(UpdatePositionSkillCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
}
