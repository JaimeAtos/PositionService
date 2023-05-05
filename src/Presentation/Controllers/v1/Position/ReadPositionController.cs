using Application.Features.Positions.Queries.GetALlPosition;
using Application.Features.Positions.Queries.GetPositionById;
using Application.Parameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class ReadPositionController : BaseApiController
{
	public ReadPositionController(IMediator mediator) : base(mediator)
	{
	}
	
	[HttpGet("{id}", Name = "GetPositionById")]
	public async Task<IActionResult> GetPositionById(Guid id)
	{
		return Ok(await Mediator.Send(new GetPositionByIdQuery{Id = id}));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllPositions([FromQuery] PositionParameters filters)
	{
		return Ok(await Mediator.Send(new GetAllPositionQuery
		{
			PageNumber = filters.PageNumber,
			PageSize = filters.PageSize,
			State = filters.State,
			Description = filters.Description,
			CatalogLevelDescription = filters.CatalogLevelDescription,
			CatalogLevelId = filters.CatalogLevelId,
			ClientDescription = filters.ClientDescription,
			ClientId = filters.ClientId
		}));
	}

}
