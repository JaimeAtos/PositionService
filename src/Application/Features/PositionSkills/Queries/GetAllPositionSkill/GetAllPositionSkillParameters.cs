using Application.Parameters;

namespace Application.Features.PositionSkills.Queries.GetAllPositionSkill;

public class GetAllPositionSkillParameters : RequestParameter
{
	public Guid? Id { get; set; }
	public bool? State { get; set; }
	public Guid? SkillId { get; set; }
	public Guid? PositionId { get; set; }
	public string? SkillName { get; set; }
	public byte? MinToAccept { get; set; }
	public byte? PositionSkillType { get; set; }
}
