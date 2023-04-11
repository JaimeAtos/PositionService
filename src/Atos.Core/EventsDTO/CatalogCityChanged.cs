using System;

namespace Atos.Core.EventsDTO
{
    /// <summary>
    /// 
    /// </summary>
    public record CatalogCityChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid CityId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid ContryId { get; set; }
    }
}
