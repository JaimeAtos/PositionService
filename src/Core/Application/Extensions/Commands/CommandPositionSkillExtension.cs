using Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;
using Application.Features.PositionSkills.Commands.DeletePositionSkillCommand;
using Application.Features.PositionSkills.Commands.UpdatePositionSkillCommand;
using Atos.Core.EventsDTO;

namespace Application.Extensions.Commands;

public static class CommandPositionSkillExtension
{
	public static PositionSkillCreated ToPositionSkillCreated(this CreatePositionSkillCommand request, Guid id)
	{
		return new PositionSkillCreated
		{
			Id = id,
			MinToAccept = request.MinToAccept,
			PositionId = request.PositionId,
			PositionSkillType = request.PositionSkillType.ToString(),
			SkillId = request.SkillId,
			SkillName = request.SkillName
		};
	}

	public static PositionSkillUpdated ToPositionSkillUpdated(this UpdatePositionSkillCommand request)
	{
		return new PositionSkillUpdated
		{
			Id = request.Id,
			MinToAccept = request.MinToAccept,
			PositionId = request.PositionId,
			PositionSkillType = request.PositionSkillType.ToString(),
			SkillId = request.SkillId,
			SkillName = request.SkillName
		};
	}

	public static PositionSkillDeleted ToPositionSkillDeleted(this DeletePositionSkillCommand request)
	{
		return new PositionSkillDeleted
		{
			Id = request.Id
		};
	}
}
