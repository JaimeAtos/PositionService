using Application.Features.Positions.Queries.GetALlPosition;
using Application.Features.Positions.Queries.GetPositionById;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class ReadPositionController : BaseApiController
{
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetPositionById(Guid id)
	{
		return Ok(await Mediator.Send(new GetPositionByIdQuery{Id = id}));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllPositions([FromQuery] GetAllPositionParameters filters)
	{
		return Ok(await Mediator.Send(new GetAllPositionParameters
		{
			PageNumber = filters.PageNumber,
			PageSize = filters.PageSize,
			Id = filters.Id,
			State = filters.State,
			ClientId = filters.ClientId,
			ClientDescription = filters.ClientDescription,
			Description = filters.Description
		}));
	}
}
