using Application.Exceptions;
using Application.Features.PositionSkills.Commands.UpdatePositionSkillCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.PositionSkill;

[ApiVersion("1.0")]
public class UpdatePositionSkillController : BaseApiController
{
    public UpdatePositionSkillController(IMediator mediator) : base(mediator)
    {
    }
  
    [HttpPut]
    public Task<IActionResult> UpdatePositionSkill(UpdatePositionSkillCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ApiException("Body request is empty");
        }

        return ProcessUpdatePositionSkill(command, cancellationToken);
    }
    private async Task<IActionResult> ProcessUpdatePositionSkill(UpdatePositionSkillCommand command, CancellationToken cancellationToken = default)
    {
        await Mediator.Send(command, cancellationToken);
        return NoContent();
    }

}
