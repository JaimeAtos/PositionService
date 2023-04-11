using Application.Parameters;

namespace Application.Features.Positions.Queries.GetALlPosition;

public class GetAllPositionParameters : RequestParameter
{
    public Guid Id { get; set; }
    public bool State { get; set; }
    public string? Description { get; set; }
    public Guid? ClientId { get; set; }
    public string? ClientDescription { get; set; }
}
