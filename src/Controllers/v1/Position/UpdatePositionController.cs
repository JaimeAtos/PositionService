using Application.Features.Positions.Commands.UpdatePositionCommand;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class UpdatePositionController : BaseApiController
{
  
    [HttpPut]
    public Task<IActionResult> UpdatePosition(UpdatePositionCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
            throw new ArgumentNullException();

        return ProcessUpdatePosition(command, cancellationToken);
    }
    private async Task<IActionResult> ProcessUpdatePosition(UpdatePositionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
}