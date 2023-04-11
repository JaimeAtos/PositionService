using System;

namespace Atos.Core.EventsDTO
{
    /// <summary>
    /// 
    /// </summary>
    public record CatalogLocationChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid LocationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CityId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CountryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CountryDescription { get; set; }
    }
}
