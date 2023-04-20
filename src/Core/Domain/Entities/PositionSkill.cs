using Atos.Core.Common;
using Domain.Enums;

namespace Domain.Entities;

public class PositionSkill: EntityBaseAuditable<Guid, Guid>
{
    public Guid SkillId { get; set; }
    public Guid PositionId { get; set; }
    public string? SkillName { get; set; }
    public byte? MinToAccept { get; set; } //100%
    public PositionSkillType PositionSkillType { get; set; }
}
