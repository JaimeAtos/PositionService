using Atos.Core.Common;

namespace Domain.Entities;

public class PositionSkill: EntityBaseAuditable<Guid, Guid>
{
    public Guid SkillId { get; set; }
    public Guid PositionId { get; set; }
    public string SkillName { get; set; }
    public byte? MinToAccept { get; set; } //100%
    public byte PositionSkillType { get; set; }
}
