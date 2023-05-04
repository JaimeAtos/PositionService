using Application.Features.ResourcePositions.Commands.CreateResourcePositionCommand;
using Application.Features.ResourcePositions.Commands.DeleteResourcePositionCommand;
using Application.Features.ResourcePositions.Commands.UpdateResourcePositionCommand;
using Atos.Core.EventsDTO;

namespace Application.Extensions.Commands;

public static class CommandResourePositionExtension
{
	public static ResourcePositionCreated ToResourcePositionCreated(this CreateResourcePositionCommand request, Guid id)
	{
		return new ResourcePositionCreated
		{
			Id = id,
			IsDefault = request.IsDefault,
			PercentMatchPosition = request.PercentMatchPosition,
			PositionId = request.PositionId,
			ResourceId = request.ResourceId,
			ResourceName = request.ResourceName,
			RomaId = request.RomaId
		};
	}

	public static ResourcePositionUpdated ToResourcePositionUpdated(this UpdateResourcePositionCommand request)
	{
		return new ResourcePositionUpdated
		{
			Id = request.Id,
			IsDefault = request.IsDefault,
			PercentMatchPosition = request.PercentMatchPosition,
			PositionId = request.PositionId,
			ResourceId = request.ResourceId,
			ResourceName = request.ResourceName,
			RomaId = request.RomaId
		};
	}

	public static ResourcePositionDeleted ToResourcePositionDeleted(this DeleteResourcePositionCommand request)
	{
		return new ResourcePositionDeleted
		{
			Id = request.Id
		};
	}
}
