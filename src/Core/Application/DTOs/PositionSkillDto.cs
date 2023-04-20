using Domain.Enums;

namespace Application.DTOs
{
	public class PositionSkillDto
	{
		public Guid Id { get; set; }
		public Guid SkillId { get; set; }
		public Guid PositionId { get; set; }
		public string? SkillName { get; set; }
		public byte? MinToAccept { get; set; }
		public PositionSkillType PositionSkillType { get; set; }
	}
}