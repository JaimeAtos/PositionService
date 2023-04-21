using Application.Features.ResourcePositions.Queries.GetAllResourcePosition;

namespace Application.Extensions;

public static class GetAllResourcePositionExtension
{
	public static Dictionary<string, object> GenerateParameters(this GetAllResourcePositionQuery query)
	{
		var filters = new Dictionary<string, object>();
		if (query.IsDefault is not null)
			filters.Add("IsDefault", query.IsDefault);

		if (query.ResourceName is not null)
			filters.Add("ResourceName", query.ResourceName);

		if (query.RomaId is not null)
			filters.Add("RomaId", query.RomaId);

		if (query.PercentMatchPosition is not null)
			filters.Add("PercentMatchPosition", query.PercentMatchPosition);
		
		if (query.ResourceId is not null)
			filters.Add("ResourceId", query.ResourceId);
		
		if (query.PositionId is not null)
			filters.Add("PositionId", query.PositionId);

		filters.Add("State", query.State);
		return filters;
	}
}
