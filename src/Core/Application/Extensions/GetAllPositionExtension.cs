
using Application.Features.Positions.Queries.GetALlPosition;

namespace Application.Extensions;

public static class GetAllPositionExtension
{
	public static Dictionary<string, object> GenerateParams(this GetAllPositionQuery query)
	{
		var filters = new Dictionary<string, object>();
		
		if (query.PositionLevel is not null)
			filters.Add("PositionLevel", query.PositionLevel);
		
		if (query.Description is not null)
			filters.Add("Description", query.Description);
		
		if (query.ClientDescription is not null)
			filters.Add("ClientDescription", query.ClientDescription);
		
		filters.Add("State", query.State);
		return filters;
	}
}
