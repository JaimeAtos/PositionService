using Domain.Entities;

namespace Application.DTOs;

public class PositionDto
{
	public Guid Id { get; set; }
	public bool State { get; set; }
	public string? Description { get; set; }
	public string? CatalogLevelDescription { get; set; }
	public string? ClientDescription { get; set; }
	public Guid CatalogLevelId { get; set; }
	public Guid ClientId { get; set; }
}
