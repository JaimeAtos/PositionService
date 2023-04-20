namespace Application.Parameters;

public class PositionParameters : RequestParameter
{
	public string? Description { get; set; }
	public bool State { get; set; }
	public Guid ClientId { get; set; }
	public string? ClientDescription { get; set; }
	public string? PositionLevel { get; set; }
}
