using Atos.Core.Common;

namespace Domain.Entities;

public class Position : EntityBaseAuditable<Guid, Guid>
{
    public string Description { get; set; }
    public Guid  ClientId { get; set; }
    public string ClientDescription { get; set; }
    public string PositionLevel { get; set; }
}
