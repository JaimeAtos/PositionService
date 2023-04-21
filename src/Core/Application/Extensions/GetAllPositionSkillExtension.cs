using Application.Features.PositionSkills.Queries.GetAllPositionSkill;

namespace Application.Extensions;

public static class GetAllPositionSkillExtension
{

	public static Dictionary<string, object> GenerateParams(this GetAllPositionSkillQuery query)
	{
		var filters = new Dictionary<string, object>();
		
		if (query.SkillName is not null)
			filters.Add("SkillName", query.SkillName);
		
		if (query.MinToAccept is not null)
			filters.Add("MinToAccept", query.MinToAccept);

		if (query.PositionSkillType is not null)
			filters.Add("PositionSkillType", query.PositionSkillType);
		
		if (query.SkillId is not null)
			filters.Add("SkillId", query.SkillId);
		
		if (query.PositionId is not null)
			filters.Add("PositionId", query.PositionId);
		
		filters.Add("State", query.State);
		return filters;
	}
}
