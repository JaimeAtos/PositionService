using Application.Exceptions;
using Application.Features.ResourcePositions.Commands.CreateResourcePositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.ResourcePosition;

[ApiVersion("1.0")]
public class CreateResourcePositionController : BaseApiController
{
    public CreateResourcePositionController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public Task<IActionResult> CreateResourcePosition(CreateResourcePositionCommand command, CancellationToken cancellation = default)
    {
        if (command is null)
        {
            throw new ApiException("Body request is empty");
        }

        return ProcessCreateResourcePosition(command, cancellation);
    }

    private async Task<IActionResult> ProcessCreateResourcePosition(CreateResourcePositionCommand command, CancellationToken cancellation = default)
    {
        var result = await Mediator.Send(command, cancellation);
        return CreatedAtRoute("GetResourcePositionById", new { id = result }, command);
    }

}