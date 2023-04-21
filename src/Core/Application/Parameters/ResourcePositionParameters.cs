namespace Application.Parameters;

public class ResourcePositionParameters : RequestParameter
{
    public Guid Id { get; set; }
    public bool State { get; set; }
    public Guid? ResourceId { get; set; }
    public Guid? PositionId { get; set; }
    public byte? PercentMatchPosition { get; set; }
    public bool? IsDefault { get; set; }
    public string? ResourceName { get; set; }
    public string? RomaId { get; set; }
}
