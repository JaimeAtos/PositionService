using System;

namespace Atos.Core.EventsDTO
{
    public record PositionChangedFullDto
    {
        public Guid PositionId { get; set; }
        public string? Description { get; set; }
        public Guid ClientId { get; set; }
        public string? ClientDescription { get; set; }
        public string? PositionLevel { get; set; }
    }
}