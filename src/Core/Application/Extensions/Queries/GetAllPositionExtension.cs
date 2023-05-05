
using Application.Features.Positions.Queries.GetALlPosition;

namespace Application.Extensions.Queries;

public static class GetAllPositionExtension
{
	public static Dictionary<string, object> GenerateParams(this GetAllPositionQuery query)
	{
		var filters = new Dictionary<string, object>();
		
		if (query.CatalogLevelDescription is not null)
			filters.Add("CatalogLevelDescription", query.CatalogLevelDescription);
		
		if (query.Description is not null)
			filters.Add("Description", query.Description);
		
		if (query.ClientDescription is not null)
			filters.Add("ClientId", query.ClientDescription);
		
		if (query.CatalogLevelId is not null)
			filters.Add("CatalogLevelId", query.CatalogLevelId);
		
		if (query.ClientId is not null)
			filters.Add("ClientId", query.ClientId);
		
		filters.Add("State", query.State);
		return filters;
	}
}
