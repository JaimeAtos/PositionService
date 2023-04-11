using Application.Features.ResourcePositions.Commands.UpdateResourcePositionCommand;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.ResourcePosition;

[ApiVersion("1.0")]
public class UpdateResourcePositionController : BaseApiController
{
    [HttpPut]
    public Task<IActionResult> UpdateResourcePosition(UpdateResourcePositionCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
            throw new ArgumentNullException();

        return ProcessUpdateResourcePosition(command, cancellationToken);
    }

    private async Task<IActionResult> ProcessUpdateResourcePosition(UpdateResourcePositionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
