using Atos.Core.Common;

namespace Domain.Entities;

public class Position : EntityBaseAuditable<Guid, Guid>
{
    public string? Description { get; set; }
    public string? CatalogLevelDescription { get; set; }
    public Guid CatalogLevelId { get; set; }
}
