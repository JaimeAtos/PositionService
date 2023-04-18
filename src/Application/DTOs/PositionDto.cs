using Domain.Entities;

namespace Application.DTOs;

public class PositionDto
{
	public Guid Id { get; set; }
	public string? Description { get; set; }
	public Guid? ClientId { get; set; }
	public string? PositionDescription { get; set; }
	public string? PositionLevel { get; set; }
	public IEnumerable<PositionSkillDto>? MinToHave { get; set; }
	public IEnumerable<PositionSkillDto>? MustToHave { get; set; }
	public IEnumerable<PositionSkillDto>? PlusToHave { get; set; }
}
