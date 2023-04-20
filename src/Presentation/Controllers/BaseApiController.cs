using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
    protected IMediator Mediator { get; }

    public BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }
}
