using Atos.Core.Commons;

namespace Domain.Entities;

public class ResourcePosition : EntityBaseAuditable<Guid, Guid>
{
    public Guid ResourceId { get; set; }
    public Guid PositionId { get; set; }
    public byte PercentMathPosition { get; set; } //20%
    public bool IsDefault { get; set; }
    public string? ResourceName { get; set; }
}
