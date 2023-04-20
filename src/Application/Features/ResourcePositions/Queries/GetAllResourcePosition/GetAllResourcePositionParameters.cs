using Application.DTOs;
using Application.Parameters;

namespace Application.Features.ResourcePositions.Queries.GetAllResourcePosition;

public class GetAllResourcePositionParameters : RequestParameter
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Guid PositionId { get; set; }
    public byte PercentMathPosition { get; set; }
    public bool IsDefault { get; set; }
    public string? ResourceName { get; set; }
    public PositionDto? Position { get; set; }
}