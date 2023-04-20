using Domain.Entities;

namespace Application.DTOs;

public class PositionDto
{
	public Guid Id { get; set; }
	public bool State { get; set; }
	public string? Description { get; set; }
	public Guid? ClientId { get; set; }
	public string? ClientDescription { get; set; }
	public string? PositionLevel { get; set; }
}
