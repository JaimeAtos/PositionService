using Application.Features.ResourcePositions.Queries.GetAllResourcePosition;
using Application.Features.ResourcePositions.Queries.GetResourcePositionById;
using Application.Parameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.ResourcePosition;

[ApiVersion("1.0")]
public class ReadResourcePositionController : BaseApiController
{
	public ReadResourcePositionController(IMediator mediator) : base(mediator)
	{
	}
	
	[HttpGet("{id}", Name = "GetResourcePositionById")]
	public async Task<IActionResult> GetResourcePositionById(Guid id)
	{
		return Ok(await Mediator.Send(new GetResourcePositionByIdQuery { Id = id }));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllResourcePositions([FromQuery] ResourcePositionParameters filters)
	{
		return Ok(await Mediator.Send(new GetAllResourcePositionQuery
		{
			PageNumber = filters.PageNumber,
			PageSize = filters.PageSize,
			State = filters.State,
			ResourceId = filters.ResourceId,
			PositionId = filters.PositionId,
			ResourceName = filters.ResourceName,
			PercentMatchPosition = filters.PercentMatchPosition,
			IsDefault = filters.IsDefault,
			RomaId = filters.RomaId
		}));
	}

}
