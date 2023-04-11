namespace Application.DTOs;

public class ResourcePositionDto
{
    public Guid Id { get; set; }
    public bool State { get; set; }
    public Guid ResourceId { get; set; }
    public Guid PositionId { get; set; }
    public byte PercentMathPosition { get; set; }
    public bool IsDefault { get; set; }
    public string ResourceName { get; set; }
}
