using Application.Exceptions;
using Application.Features.Positions.Commands.UpdatePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class UpdatePositionController : BaseApiController
{
    
    public UpdatePositionController(IMediator mediator) : base(mediator)
    {
    }
  
    [HttpPut]
    public Task<IActionResult> UpdatePosition(UpdatePositionCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ApiException("Body request is empty");
        }

        return ProcessUpdatePosition(command, cancellationToken);
    }
    private async Task<IActionResult> ProcessUpdatePosition(UpdatePositionCommand command, CancellationToken cancellationToken = default)
    {
        await Mediator.Send(command, cancellationToken);
        return NoContent();
    }

}