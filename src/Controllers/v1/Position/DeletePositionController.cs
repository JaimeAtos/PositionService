using Application.Features.Positions.Commands.DeletePositionCommand;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class DeletePositionController : BaseApiController
{
    [HttpDelete]
    public Task<IActionResult> DeletePosition(DeletePositionCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
            throw new ArgumentNullException();

        return ProcessDeletePosition(command, cancellationToken);
    }
    private async Task<IActionResult> ProcessDeletePosition(DeletePositionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
}