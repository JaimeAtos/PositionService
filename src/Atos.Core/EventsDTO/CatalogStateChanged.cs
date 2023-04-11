using System;

namespace Atos.Core.EventsDTO
{

    /// <summary>
    /// 
    /// </summary>
    public record CatalogStateChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid CatalogId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}

