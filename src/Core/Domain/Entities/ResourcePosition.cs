using Atos.Core.Common;

namespace Domain.Entities;

public class ResourcePosition : EntityBaseAuditable<Guid, Guid>
{
    public byte PercentMatchPosition { get; set; }
    public bool IsDefault { get; set; }
    public string? ResourceName { get; set; }
    public Guid ResourceId { get; set; }
    public Guid PositionId { get; set; }
    public string? RomaId { get; set; }
}
