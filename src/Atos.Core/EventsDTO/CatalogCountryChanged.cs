
using System;

namespace Atos.Core.EventsDTO
{
    /// <summary>
    /// 
    /// </summary>
    public record CatalogCountryChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid CoutryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

    }
}

