namespace Application.Parameters;

public class PositionParameters : RequestParameter
{
	public bool State { get; set; }
	public string? Description { get; set; }
	public string? CatalogLevelDescription { get; set; }
	public Guid? CatalogLevelId{ get; set; }
}
