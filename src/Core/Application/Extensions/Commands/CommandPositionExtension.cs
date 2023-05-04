using Application.Features.Positions.Commands.CreatePositionCommand;
using Application.Features.Positions.Commands.DeletePositionCommand;
using Application.Features.Positions.Commands.UpdatePositionCommand;
using Atos.Core.EventsDTO;

namespace Application.Extensions.Commands;

public static class CommandPositionExtension
{
	public static PositionCreated ToPositionCreated(this CreatePositionCommand request, Guid id)
	{
		return new PositionCreated
		{
			Id = id,
			CatalogLevelDescription = request.CatalogLevelDescription,
			CatalogLevelId = request.CatalogLevelId,
			Description = request.Description
		};
	}
	
	public static PositionUpdated ToPositionUpdated(this UpdatePositionCommand request)
	{
		return new PositionUpdated
		{
			Id = request.Id,
			CatalogLevelDescription = request.CatalogLevelDescription,
			CatalogLevelId = request.CatalogLevelId,
			Description = request.Description
		};
	}
	
	public static PositionDeleted ToPositionDeleted(this DeletePositionCommand request)
	{
		return new PositionDeleted
		{
			Id = request.Id
		};
	}
}
